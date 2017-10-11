//入金申请数据验证
function DepositDateCheck() {

    var money = $("#txtMoney").val(); //金额
    var exp = /^([1-9][\d]{0,9}|0)(\.[\d]{1,2})?$/;
    if (exp.test(money)) {
    } else {
        alert('请输入正确的金额数，最多保留2位小数！');
        return false;
    }
    //var Email = $("#txtEmail").val(); //Email
    //var email_test = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    //if (email_test.test(Email)) {
    //} else {
    //    alert("请输入正确的邮箱地址~！如：info@dmgfx.com");
    //    return false;
    //}

    var Account = delHtmlTag($("#txtAccouont").val()); //账户
    var Address = delHtmlTag($("#txtAddress").val()); //地址
    
    var Name = delHtmlTag($("#txtName").val()); //姓名
    var Phone = delHtmlTag($("#txtPhone").val()); //电话
    var Supplement = delHtmlTag($("#txtExplain").val()); //补充说明
    if (Account == "" || Address == ""|| Name == "" || Phone == "" || Supplement == "") {
        alert("请填写所有信息以方便我们与您联系，谢谢~！");
        return false;
    } 

    return true;
}

