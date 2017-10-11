
function InitTables() {
    BindSelect("#country", Country);
    BindSelect("#country2", Country);
    BindSelect("#country3", Country);
    $(".closeaccount").hide(); $(".TransitBankInfo").hide();
    $("#IsCloseAccountY").click(function () { $(".closeaccount").show(); });
    $("#IsCloseAccountN").click(function () { $(".closeaccount").hide(); });
    $("#RadioIsTransitBankY").click(function () { $(".TransitBankInfo").show(); })
    $("#RadioIsTransitBankN").click(function () { $(".TransitBankInfo").hide(); })
    $.ajax({
        type: "post", async: true, url: '../Agency/GetLoginWithdraw',
        success: function (data) {
            var item = eval(data);
            if (item.length < 0) {
            } else {
                $("#country").val(item[0].WithdrawalsCountry);
                $("#country2").val(item[0].BankCountry)
                $("#country3").val(item[0].TransitBankCountry)
                $("#WWAccountHolder").val(item[0].WithdrawalsAccountHolder);
                $("#WWTrading_account").val(item[0].WithdrawalsAccount);
                $("#WWPhone").val(item[0].Phone);
                $("#WWEmail").val(item[0].WithdrawalsEmail);
                $("#WWAddress").val(item[0].WithdrawalsAddress);
                $("#WWProvince").val(item[0].WithdrawalsProvince);
                $("#WWCity").val(item[0].WithdrawalsCity);
                $("#WWZipCode").val(item[0].WithdrawalsCityZipCode);
                $("#WWMoney").val("");
                $("#IsCloseAccountText").val(item[0].CloseReason);
                $("#WWBankName").val(item[0].BankName);
                $("#WWBankAccount").val(item[0].BankAccount);
                $("#AccountHolderName").val(item[0].BankHolder);
                $("#WWSWIFT_Code").val(item[0].SWIFT_Code);
                $("#WWBankAddress").val(item[0].BankAddress);
                $("#WWBankCity").val(item[0].BankCity);
                $("#WWBankProvince").val(item[0].BankProvince);
                $("#WWBankZipCode").val(item[0].BankCityZipCode);
                $("#WWTransitBankName").val(item[0].TransitBankName);
                $("#WWTransitBankAcoount").val(item[0].TransitBankAccount);
                $("#WWTransitAcoountHolderName").val(item[0].TransitBankHolder);
                $("#WWTransitSWIFTCode").val(item[0].TransitBankSWIFICode);
                $("#WWTransitBankAddress").val(item[0].TransitBankAddress);
                $("#WWTransitProvince").val(item[0].TransitBankProvince);
                $("#WWTransitCity").val(item[0].TransitBankCity);
                $("#WWTransitZipCode").val(item[0].TransitBankCityZipCode);
                $("#WWMainAccountHolderName").val(item[0].MainAccountName);
            }
        }
    })
}

//在线取款申请表
function CheckWebApplyData() {
    /*******************************************************************************/
    var HolderName = $("#WWAccountHolder").val(); //账户持有人姓名
    if (HolderName.replace(/(^\s*)|(\s*$)/g, "") == "") {
        $("#WWAccountHolder").parents(".form-group").addClass("has-error");
        return false;
    }
    var Trading_account = $("#WWTrading_account").val(); //交易账户
    if (Trading_account.replace(/(^\s*)|(\s*$)/g, "") == "") {
        $("#WWTrading_account").parents(".form-group").addClass("has-error");
        return false;
    }
    var WWEmail = $("#WWEmail").val(); //电子邮箱
    var WWEmailexp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!WWEmailexp.test(WWEmail)) {
        $("#WWEmail").parents(".form-group").addClass("has-error");
        return false;
    }
    var WWMoney = $("#WWMoney").val(); //取款金额
    var exp = /^([1-9][\d]{0,7}|0)(\.[\d]{1,2})?$/;
    if (!exp.test(WWMoney)) {
        $("#WWMoney").parents(".form-group").addClass("has-error");
        return false;
    }

    //var WWCountry = $("#_Country1").val(); //国家
    //if (WWCountry == "_SELECT_COUNTRY") {
    //    $("#WWAccountHolder").parents(".form-group").addClass("has-error");
    //    return false;
    //}
    /*******************************************************************************/

    var WWBankName = $("#WWBankName").val(); //银行名称
    if (WWBankName.replace(/(^\s*)|(\s*$)/g, "") == "") {
        $("#WWBankName").parents(".form-group").addClass("has-error");
        return false;
    }
    var WWBankAccount = $("#WWBankAccount").val(); //银行账号
    if (WWBankAccount.replace(/(^\s*)|(\s*$)/g, "") == "") {
        $("#WWBankAccount").parents(".form-group").addClass("has-error");
        return false;
    }
    var AccountHolderName = $("#AccountHolderName").val(); //账户持有人姓名
    if (AccountHolderName.replace(/(^\s*)|(\s*$)/g, "") == "") {
        $("#AccountHolderName").parents(".form-group").addClass("has-error");
        return false;
    }
    var WWSWIFT_Code = $("#WWSWIFT_Code").val(); //SWIFT Code(*)国际银行代码
    if (WWSWIFT_Code.replace(/(^\s*)|(\s*$)/g, "") == "") {
        $("#WWSWIFT_Code").parents(".form-group").addClass("has-error");
        return false;
    }
    var WWBankAddress = $("#WWBankAddress").val(); //银行地址
    if (WWBankAddress.replace(/(^\s*)|(\s*$)/g, "") == "") {
        $("#WWBankAddress").parents(".form-group").addClass("has-error");
        return false;
    }

    //var WWBankCountry = $("#_Country2").val(); //银行所在国家
    //if (WWBankCountry == "_SELECT_COUNTRY") {
    //    $("#WWAccountHolder").parents(".form-group").addClass("has-error");
    //    return false;
    //}
    /*******************************************************************************/
    if ($("#RadioIsTransitBankY").attr("checked")) {

        var WWTransitBankName = $("#WWTransitBankName").val(); //中转银行名称
        if (WWTransitBankName.replace(/(^\s*)|(\s*$)/g, "") == "") {
            $("#WWTransitBankName").parents(".form-group").addClass("has-error");
            return false;
        }
        var WWTransitBankAcoount = $("#WWTransitBankAcoount").val(); //中转银行账号
        if (WWTransitBankAcoount.replace(/(^\s*)|(\s*$)/g, "") == "") {
            $("#WWTransitBankAcoount").parents(".form-group").addClass("has-error");
            return false;
        }
        var WWTransitAcoountHolderName = $("#WWTransitAcoountHolderName").val(); //中转账户持有人姓名
        if (WWTransitAcoountHolderName.replace(/(^\s*)|(\s*$)/g, "") == "") {
            $("#WWTransitAcoountHolderName").parents(".form-group").addClass("has-error");
            return false;
        }
        var WWTransitSWIFTCode = $("#WWTransitSWIFTCode").val(); //中转SWIFT Code(*)国际银行代码

        if (WWTransitSWIFTCode.replace(/(^\s*)|(\s*$)/g, "") == "") {
            $("#WWTransitSWIFTCode").parents(".form-group").addClass("has-error");
            return false;
        }
        var WWTransitBankAddress = $("#WWTransitBankAddress").val(); //中转银行地址
        if (WWTransitBankAddress.replace(/(^\s*)|(\s*$)/g, "") == "") {
            $("#WWTransitBankAddress").parents(".form-group").addClass("has-error");
            return false;
        }

        //var WWTransitCountry = $("#_Country3").val();
        //if (WWTransitCountry == "_SELECT_COUNTRY") {
        //    $("#WWAccountHolder").parents(".form-group").addClass("has-error");
        //    return false;
        //}

    }
    /*******************************************************************************/

    var WWMainAccountHolderName = $("#WWMainAccountHolderName").val(); //主要账户持有人姓名
    if (WWMainAccountHolderName.replace(/(^\s*)|(\s*$)/g, "") == "") {
        $("#WWMainAccountHolderName").parents(".form-group").addClass("has-error");
        return false;
    }

    return true;
}


function SubmitWebApplyData() {
    var HolderName = $("#WWAccountHolder").val(); //账户持有人姓名
    var WWTrading_account = $("#WWTrading_account").val(); //交易账号
    var WWPhone = $("#WWPhone").val(); //联系电话
    var WWEmail = $("#WWEmail").val(); //电子邮箱
    var WWAddress = $("#WWAddress").val(); //地址
    var WWCountry = $("#country").val(); //个人居住国家
    var WWProvince = $("#WWProvince").val(); //个人居住州
    var WWCity = $("#WWCity").val(); //个人居住城市
    var WWZipCode = $("#WWZipCode").val(); //个人居住城市邮编
    var WWMoney = $("#WWMoney").val(); //取款金额
    //var WWAccountHolder = $(".WWAccountHolder").val(); //平台meta trader 4
    var WWAccountHolder = $("input[name='ckdPlatformName']:checked").val();
    //var WWAccountHolder = 'meta trader 4'; //平台meta trader 4
    var IsCloseAccountY = $("#IsCloseAccountY").attr("checked") ? "1" : "0"; //是否关闭交易账户
    var IsCloseAccountText = $("#IsCloseAccountText").val(); //关闭账户原因
    var WWBankName = $("#WWBankName").val(); //银行名称
    var WWBankAccount = $("#WWBankAccount").val(); //银行账号
    var AccountHolderName = $("#AccountHolderName").val(); //银行账户持有人姓名
    var WWSWIFT_Code = $("#WWSWIFT_Code").val(); //国际银行代码（SWIFT Code）
    var WWBankAddress = $("#WWBankAddress").val(); //银行地址
    var WWBankCountry = $("#country2").val(); //银行所在国家
    var WWBankProvince = $("#WWBankProvince").val(); //银行所在州，省
    var WWBankCity = $("#WWBankCity").val(); //银行所在城市
    var WWBankZipCode = $("#WWBankZipCode").val(); //银行所在城市邮编
    var RadioIsTransitBankN = $("#RadioIsTransitBankY").attr("checked") ? "1" : "0"; //是否有中转银行
    var WWTransitBankName = $("#WWTransitBankName").val(); //中转银行名称
    var WWTransitBankAcoount = $("#WWTransitBankAcoount").val(); //中转银行账号
    var WWTransitAcoountHolderName = $("#WWTransitAcoountHolderName").val(); //中转银行账户持有人姓名
    var WWTransitSWIFTCode = $("#WWTransitSWIFTCode").val(); //中转银行国际银行代码
    var WWTransitBankAddress = $("#WWTransitBankAddress").val(); //中转银行地址
    var WWTransitCountry = $("#country3").val(); //中转银行所在国家
    var WWTransitProvince = $("#WWTransitProvince").val(); //中转银行所在州省
    var WWTransitCity = $("#WWTransitCity").val(); //中转银行所在城市
    var WWTransitZipCode = $("#WWTransitZipCode").val(); //中转银行所在城市右邮编
    var WWMainAccountHolderName = $("#WWMainAccountHolderName").val(); //主要账户持有人姓名
    //var WWMinorAccountHolderName = $(".WWMinorAccountHolderName").val(); //次要账户持有人姓名

    $.ajax({
        type: "post", async: true, url: '../Agency/AccountWithdrawals',
        data: {
            "HolderName": HolderName, "WWTrading_account": WWTrading_account, "WWPhone": WWPhone, "WWEmail": WWEmail, "WWAddress": WWAddress, "WWCountry": WWCountry, "WWProvince": WWProvince,
            "WWCity": WWCity, "WWZipCode": WWZipCode, "WWMoney": WWMoney, "WWAccountHolder": WWAccountHolder, "IsCloseAccountY": IsCloseAccountY, "IsCloseAccountText": IsCloseAccountText,
            "WWBankName": WWBankName, "WWBankAccount": WWBankAccount, "AccountHolderName": AccountHolderName, "WWSWIFT_Code": WWSWIFT_Code, "WWBankAddress": WWBankAddress, "WWBankCountry": WWBankCountry,
            "WWBankProvince": WWBankProvince, "WWBankCity": WWBankCity, "WWBankZipCode": WWBankZipCode, "RadioIsTransitBankN": RadioIsTransitBankN, "WWTransitBankName": WWTransitBankName,
            "WWTransitBankAcoount": WWTransitBankAcoount, "WWTransitAcoountHolderName": WWTransitAcoountHolderName, "WWTransitSWIFTCode": WWTransitSWIFTCode, "WWTransitBankAddress": WWTransitBankAddress,
            "WWTransitCountry": WWTransitCountry, "WWTransitProvince": WWTransitProvince, "WWTransitCity": WWTransitCity, "WWTransitZipCode": WWTransitZipCode, "WWMainAccountHolderName": WWMainAccountHolderName
            //"WWMinorAccountHolderName": WWMinorAccountHolderName
        }, success: function (data) {
            if (data == 1) {
                $("#modal_withdraw").modal('show');
                setTimeout(function () { $("#modal_withdraw").modal('hide'); }, 2000);
            } else {
                alert("提交申请失败，请检查数据后重新提交~！");
            }
        }
    })
}


