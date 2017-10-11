function Init() {
    //判断用户是否登录
    $.ajax({
        type: "POST", async: true, url: '../Agency/GetLogin',
        success: function (data) {
            if (data != "") {
                var json = eval("(" + data + ")");
                if (json.userName.indexOf("COMMISSIONS") > 0) {
                    var sss = json.userName.split("COMMISSIONS");
                    $(".NAME").html(sss[0] + "<span class='caret'></span>");
                }
                else {
                    $(".NAME").html(json.userName + "<span class='caret'></span>");
                }

            } else {
                $("#modal_login").modal('show');
            }
        }
    });

    //退出登录
    $("#logout").click(function () {
        $.ajax({
            type: "POST", async: true, url: '../Agency/LoginOut',
            success: function () {
                window.location = "../Home/Index#account";
            }
        });
    });
}


//返回当前日期与指定期限差(0:还未到预订日期  1:正好在预订日期之内  2:已经超过预订期限)
function getTimeStamp(stime, etime) {
    var now_time = todayTime(0, 0);
    if (now_time < stime) {//还未到预订日期
        return 0;
    }
    else if (now_time >= stime && now_time <= etime)//正好在预订日期之内
    {
        return 1;
    } else {//已经超过预订期限
        return 2;
    }
}
//返回当前时间(天,月)20160303
function todayTime(addDays, addMonth) {
    var today = new Date();//定义日期对象
    today.setMonth(today.getMonth() + addMonth);
    today.setDate(today.getDate() + addDays);
    var yyyy = today.getFullYear();//通过日期对象的getFullYear()方法返回年
    var MM = today.getMonth() + 1;//通过日期对象的getMonth()方法返回年
    var dd = today.getDate();//通过日期对象的getDate()方法返回年
    MM = checkTime(MM);
    dd = checkTime(dd);
    var day = yyyy + "" + MM + "" + dd;
    return day;
}

//返回当前时间20160306120036
function getDateTime() {
    var today = new Date();//定义日期对象
    var yyyy = today.getFullYear();//通过日期对象的getFullYear()方法返回年
    var MM = today.getMonth() + 1;//通过日期对象的getMonth()方法返回年
    var dd = today.getDate();//通过日期对象的getDate()方法返回年
    var HH = today.getHours();//获取当前小时数(0-23)
    var mm = today.getMinutes();//获取当前分钟数(0-59)
    var ss = today.getSeconds();//获取当前秒数(0-59)
    MM = checkTime(MM);
    dd = checkTime(dd);
    HH = checkTime(HH);
    mm = checkTime(mm);
    ss = checkTime(ss);
    var now = yyyy + "" + MM + "" + dd + "" + HH + "" + mm + "" + ss;
    return now;

}
function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

//转换成  1,200.00  的格式
function currencyFormatter(num, precision) {
    var cents, sign;
    if (!num) num = 0;
    num = num.toString().replace(/\$|\,/g, '').replace(/[a-zA-Z]+/g, '');
    if (num.indexOf('.') > -1) num = num.replace(/[0]+$/g, '');
    if (isNaN(num))
        num = 0;
    sign = (num == (num = Math.abs(num)));

    if (precision == null) {
        num = num.toString();
        cents = num.indexOf('.') != -1 ? num.substr(num.indexOf('.') + 1) : '';
        if (cents) {
            num = Math.floor(num * 1);
            num = num.toString();
        }
    }
    else {
        precision = parseInt(precision);
        var r = Math.pow(10, precision);
        num = Math.floor(num * r + 0.50000000001);
        cents = num % 100;
        num = Math.floor(num / r).toString();
        while (cents.toString().length < precision) {
            cents = "0" + cents;
        }
    }
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
        num.substring(num.length - (4 * i + 3));
    var numStr = "" + (((sign) ? '' : '-') + '' + num);
    if (cents) numStr += ('.' + cents);
    return numStr;
}

function parseNumber(s) {
    return parseFloat(s.replace(/[^\d\.-]/g, ""));
}




//发送电子邮件  不需要账号,传入参数(type  认购Subscription    申购Purchase)
function notNeedAccount(type, ProNo, ProName, CurrencyName, Explain2) {
    $.ajax({
        type: "POST", async: true, url: '../Fund/Subscription',
        data: { "type": type, "ProNo": ProNo, "ProName": ProName, "CurrencyName": CurrencyName, "Explain2": Explain2 },
        success: function () {
           alert("操作成功,请等待客服联系!");
        }
    })
}

//再次发送邮件
function sendEmail(type, ProNo, ProName, CurrencyName, Explain2) {
    $.ajax({
        type: "POST", async: true, url: '../Fund/Subscription',
        data: { "type": type, "ProNo": ProNo, "ProName": ProName, "CurrencyName": CurrencyName, "Explain2": Explain2 },
        success: function () {
            wait.close();
            alert("您已经申请过账号,请等待客服联系!", "提示", "none");
        }
    })
}

// 根据币种查询是否有账号  返回1:已经有账号  0:未申请账号  2：已申请，客服未处理
function isExist(CurrencyId, product) {
    var exist = 0;
    $.ajax({
        type: "POST", async: false, url: '../Fund/IsExist', data: { "CurrencyId": CurrencyId, "Explain2": product },
        success: function (data) { exist = data; }
    })
    return exist
}

//申请账号   类型,产品代码，产品名称，货币对名称，货币对主键
function insertAccount(type, ProNo, ProName, CurrencyName, CurrencyId, explain2) {
    $.ajax({
        type: "POST", async: true, url: '../Fund/InsertAccount',
        data: { "type": type, "ProNo": ProNo, "ProName": ProName, "CurrencyName": CurrencyName, "CurrencyId": CurrencyId, "Explain2": explain2 },
        success: function (data) {
            if (data == "success") {
                alert("账号申请成功,请等待客服联系!");
            }
        }, error: function () { alert("error"); }
    })

}






var equityValue = new Array();
var equityDate = new Array();
function Chats(equityType) {
    var title = equityType == 1 ? "看涨" : "净值走势图看跌";
    $.ajax({
        type: "POST", async: false, url: '../Fund/GetAllEquity', data: { "equityType": equityType },
        success: function (equity) {
            var json = eval(equity);
            equityValue = new Array();
            equityDate = new Array();
            for (var i = 0; i < json.length; i++) {
                equityValue.push(parseFloat(json[i].EquityValue));
                equityDate.push(json[i].EquityDate);
            }
            $('#container').highcharts({
                chart: {
                    zoomType: 'x',//托送鼠标放大方向
                    spacingRight: 20
                },
                title: {
                    text: '净值走势图' + title
                },
                subtitle: {
                    text: document.ontouchstart === undefined ?
                        '单击并在绘图区拖动放大' :
                        'Click and drag in the plot area to zoom in'
                },
                xAxis: {
                    //categories:equityDate
                    type: 'datetime',
                    maxZoom: 14 * 24 * 3600000, // fourteen days
                    title: {
                        text: null
                    }
                },
                yAxis: {
                    title: {
                        text: '净值(美元)'
                    }
                },
                tooltip: {
                    shared: true, valueSuffix: '美元'
                },
                legend: {
                    enabled: false
                },
                credits: {
                    enabled: false // 禁用版权信息
                },
                plotOptions: {
                    area: {
                        fillColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, Highcharts.getOptions().colors[0]],
                                [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                            ]
                        },
                        lineWidth: 1,
                        marker: {
                            enabled: false
                        },
                        shadow: false,
                        states: {
                            hover: {
                                lineWidth: 1
                            }
                        },
                        threshold: null
                    }
                },
                series: [{
                    type: 'area',
                    name: '单位净值',
                    pointInterval: 24 * 3600 * 1000,
                    pointStart: Date.UTC(2016, 2, 7),//开始时间2016年3月7日
                    data: equityValue
                }]
            });

            getProfit(json);

        }
    })
}

//计算 回报率
function getProfit(json) {
    var nowTime = getDateTime();//获取现在的时间  20160320120000
    var equityTime = todayTime(0, 0) + "120000";
    var _equityTime = nowTime > equityTime ? todayTime(0, 0) : todayTime(-1, 0);
    var equity = getEquityValue(json, _equityTime);
    $("#sp_equity").html(equity);//单位净值
    $("#lbl_equity").text(equity);
    $("#sp_totalEquity").html(equity);//累计净值
    $("#lbl_equityValue").text("净值日期:" + _equityTime);
    var yesterdayEquityTime = nowTime > equityTime ? todayTime(-1, 0) : todayTime(-2, 0);
    var yesterdayEquity = getEquityValue(json, yesterdayEquityTime);
    if (equity == "" || yesterdayEquity == "") {
        $("#sp_equity_day").html("--%");//日涨跌
        $("#lbl_equity_day").text("--%");
    } else {
        $("#sp_equity_day").html(((equity - yesterdayEquity) / yesterdayEquity).toFixed(4) + "%");
        $("#lbl_equity_day").text(((equity - yesterdayEquity) / yesterdayEquity).toFixed(4) + "%");
    }


    //一周回报率

    //获取星期几
    var weekDay = new Date(ConvertDate(nowTime)).getDay();
    var monday, friday;
    switch (weekDay) {
        case 1://星期一
            monday = getEquityValue(json, todayTime(-7, 0));
            friday = getEquityValue(json, todayTime(-3, 0));
            break;
        case 2:
            monday = getEquityValue(json, todayTime(-8, 0));
            friday = getEquityValue(json, todayTime(-4, 0));
            break;
        case 3:
            monday = getEquityValue(json, todayTime(-9, 0));
            friday = getEquityValue(json, todayTime(-5, 0));
            break;
        case 4:
            monday = getEquityValue(json, todayTime(-10, 0));
            friday = getEquityValue(json, todayTime(-6, 0));
            break;
        case 5:
            monday = getEquityValue(json, todayTime(-4, 0));
            friday = getEquityValue(json, todayTime(0, 0));
            break;
        case 6:
            monday = getEquityValue(json, todayTime(-5, 0));
            friday = getEquityValue(json, todayTime(-1, 0));
            break;
        case 0:
            monday = getEquityValue(json, todayTime(-6, 0));
            friday = getEquityValue(json, todayTime(-2, 0));
            break;
        default: break;
    }
    if (monday == "" || friday == "") {
        $("#sp_equity_week").html("--%");//一周回报率
    }
    else {
        $("#sp_equity_week").html(((friday - monday) / monday).toFixed(4) + "%");//一周回报率
    }

    //一月回报率

    var mydate = new Date();
    var year = mydate.getFullYear();//获取年份
    var month = mydate.getMonth() + 1;
    month = checkTime(month - 1);//获取上个月月份

    var monthFirst = year + "" + month + "" + "01";//上个月第一天
    mydate = new Date(year, month, 0);
    var monthLast = year + month + mydate.getDate();//上个月最后一天
    //alert(monthLast);
    var lastMonthFirst = getEquityValue(json, monthFirst);//上个月第一天的净值

    var lastMonthLast = getEquityValue(json, monthLast);//上月最后一天净值


    if (lastMonthFirst == "" || lastMonthLast == "") {//一个月回报率
        $("#sp_equity_month").html("--%");//一个月回报率
    } else {
        $("#sp_equity_month").html(((lastMonthLast - lastMonthFirst) / lastMonthFirst).toFixed(4) + "%");//一个月回报率
    }

    //三个月回报率
    mydate = new Date();
    //var a=  mydate.getDate();alert(a);
    var quarterMonthFirst = getEquityValue(json, todayTime(-mydate.getDate() + 1, -3));//三个月前的第一天
    if (quarterMonthFirst == "" || lastMonthLast == "") {
        $("#sp_equity_Quarter").html("--%");
    } else {
        $("#sp_equity_Quarter").html(((lastMonthLast - quarterMonthFirst) / quarterMonthFirst).toFixed(4) + "%");
    }

    //六个月回报率
    mydate = new Date();
    var sixMonthFirst = getEquityValue(json, todayTime(-mydate.getDate() + 1, -6));
    if (sixMonthFirst == "" || lastMonthLast == "") {
        $("#sp_equity_SixMonths").html("--%");
    } else {
        $("#sp_equity_SixMonths").html(((lastMonthLast - sixMonthFirst) / sixMonthFirst).toFixed(4) + "%");
    }

    //一年回报率
    mydate = new Date();
    var yearFirst = getEquityValue(json, todayTime(-mydate.getDate() + 1, -12));
    if (yearFirst == "" || lastMonthLast == "") {
        $("#sp_equity_year").html("--%");
    } else {
        $("#sp_equity_year").html(((lastMonthLast - yearFirst) / yearFirst).toFixed(4) + "%");
    }

}
//根据时间返回净值
function getEquityValue(json, dateTime) {
    var equityv = "";
    for (var i = 0; i < json.length; i++) {
        if (json[i].EquityDate == dateTime) {
            equityv = json[i].EquityValue;
        }
    }
    return equityv;
}

function getEquityTotal(json, stime, etime) {
    var total = 0; var timeStamp = 0;
    for (var i = 0; i < json.length; i++) {
        if (json[i].EquityDate > stime && json[i].EquityDate <= etime) {
            total += parseFloat(json[i].EquityValue);
            timeStamp += 1;
        }
    }
    return total / timeStamp;
}
