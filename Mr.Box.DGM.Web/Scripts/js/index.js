//获取用户名
$(function () {

    $.ajax({
        type: "POST",
        async: true,
        url: '../Manage/IsLoginValid',
        success: function (msg) {
            if (msg === "" || msg == null) {
                layer.alert('账户登录凭证已过期,请重新登陆!',
                    {
                        skin: 'layui-layer-molv',
                        closeBtn: 0
                    },
                    function () {
                        window.location.href = '../Manage/Login';
                    });
            } else {
                $(".login-user").text(msg);
            }
        }
    });

    startTime();
    change_Account();
   
    setInterval(function () {
        change_Account();
    }, 1800000);//30分钟执行一次
});


//清除缓存
function clearCache() {
    layer.load();
    $.ajax({
        type: "POST", async: true, url: '../Manage/ClearCache',
        success: function () {
            setTimeout(function () {
                layer.closeAll('loading');
                layer.msg('缓存已经清理完成,即将重新载入页面', { icon: 1 });
                setTimeout(function () { window.location.reload(); }, 2500);
            }, 3000);
        }
    });
}

//退出登录
function logOut() {
    layer.load();
    $.ajax({
        type: "POST", async: true, url: '../Manage/DeleteCookie',
        data: { "cookieName": "ManageUser" },
        success: function () {
            setTimeout(function () {
                window.location.href = '../Manage/Login';
            }, 3000);
        }
    });
}

function startTime() {
    var today = new Date();//定义日期对象
    var yyyy = today.getFullYear();//通过日期对象的getFullYear()方法返回年
    var MM = today.getMonth() + 1;//通过日期对象的getMonth()方法返回月
    var dd = today.getDate();//通过日期对象的getDate()方法返回日
    var hh = today.getHours();//通过日期对象的getHours方法返回小时
    var mm = today.getMinutes();//通过日期对象的getMinutes方法返回分钟
    var ss = today.getSeconds();//通过日期对象的getSeconds方法返回秒
    // 如果分钟或小时的值小于10，则在其值前加0，比如如果时间是下午3点20分9秒的话，则显示15：20：09
    MM = checkTime(MM);
    dd = checkTime(dd);
    mm = checkTime(mm);
    ss = checkTime(ss);
    var day; //用于保存星期（getDay()方法得到星期编号）
    if (today.getDay() == 0) day = "星期日 ";
    if (today.getDay() == 1) day = "星期一 ";
    if (today.getDay() == 2) day = "星期二 ";
    if (today.getDay() == 3) day = "星期三 ";
    if (today.getDay() == 4) day = "星期四 ";
    if (today.getDay() == 5) day = "星期五 ";
    if (today.getDay() == 6) day = "星期六 ";
    document.getElementById('nowDateTimeSpan').innerHTML = yyyy + "-" + MM + "-" + dd + " " + hh + ":" + mm + ":" + ss + "   " + day;
    //$("#nowDateTimeSpan").innerHTML = yyyy + "-" + MM + "-" + dd + " " + hh + ":" + mm + ":" + ss + "   " + day;
    setTimeout('startTime()', 1000);//每一秒中重新加载startTime()方法
}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

function change_Account() {
    $.ajax({
        type: "POST",
        async: true,
        url: '../Manage/changes_dbo_Account',
        success: function (msg) {
            if (msg.length !== 0) {
                var locationurl = window.location.href;
                if (locationurl.indexOf("OpenAccount") > 0) {
                    layer.alert('新的开户信息:' + msg + '', {
                        skin: 'layui-layer-molv',
                        closeBtn: 0
                    });
                }
                else {
                    layer.confirm('新的开户信息:' + msg + '是否马上处理？', {
                        skin: 'layui-layer-molv',
                        btn: ['确定', '取消'] //按钮
                    }, function () {
                        window.location = '../Manage/OpenAccount';
                    },
                   function () { }
               );
                }
            }
        }
    });
}

