
function InitTables() {
    BindSelect("#country", Country);
	BindSelect("#country1", Country);
    BindSelect("#country2", Country);
    BindSelect("#country3", Country);
    $(".closeaccount").hide(); $(".TransitBankInfo").hide();
    $("#IsCloseAccountY").click(function () { $(".closeaccount").show(); });
    $("#IsCloseAccountN").click(function () { $(".closeaccount").hide(); });
    $("#RadioIsTransitBankY").click(function () { $(".TransitBankInfo").show(); })
    $("#RadioIsTransitBankN").click(function () { $(".TransitBankInfo").hide(); })
    $.ajax({
        type: "post", async: true, url: '../Agency/GetUserForChangingForm',
        success: function (data) {
            var item = eval(data);
            if (item.length < 0) {
            } else {
                $("#country").val(item.AccountCountry);
				$("#country1").val(item.HirerCountry);
                $("#country2").val(item.AccountCountry)
                $("#country3").val(item.HirerCountry)
                $("#WWAccountHolder").val(item.AccountName);
                $("#WWAccountEnName").val(item.AccountEnName);
                $("#WWTrading_account").val(item.AccountExplanation);
				$("#WWOldAccountPhone").val(item.AccountMobile);
                $("#WWOldAccountEmail").val(item.AccountEmail);
                $("#WWOldAccountAddress").val(item.AccountAddress);
                $("#WWOldAccountProvince").val(item.AccountProvince);
				$("#WWOldHirerName").val(item.HirerName);
				$("#WWOldHirerAddress").val(item.HirerAddress);
				$("#WWOldHirerZipCode").val(item.HirerZipcode);
				$("#WWOldHirerProvince").val(item.HirerProvince);
				$("#WWNewAccountPhone").val(item.AccountMobile);
				$("#WWNewAccountEmail").val(item.AccountEmail);
				$("#WWNewAccountAddress").val(item.AccountAddress);
				$("#WWNewAccountProvince").val(item.AccountProvince);
				$("#WWNewHirerName").val(item.HirerName);
				$("#WWNewHirerAddress").val(item.HirerAddress);
				$("#WWNewHirerZipCode").val(item.HirerZipcode);
				$("#WWNewHirerProvince").val(item.HirerProvince);
            }
        }
    })
}

//在线取款申请表
function CheckWebApplyData() {
    /*******************************************************************************/
    var HolderName = $("#WWAccountHolder").val(); //账号
	
	/*原有信息*/
	var WWOldAccountPhone=$("#WWOldAccountPhone").val();	//用户手机
    var WWOldAccountEmail = $("#WWOldAccountEmail").val(); //电子邮箱
	var WWOldAccountAddress= $("#WWOldAccountAddress").val();	//账户地址
	var WWOldAccountCountry=$("#country").val();		//账户国家
	var WWOldAccountProvince=$("#WWOldAccountProvince").val();	//账户省/州
	var WWOldHirerName= $("#WWOldHirerName").val();		//雇主姓名
	var WWOldHirerAddress= $("#WWOldHirerAddress").val();	//雇主地址
	var WWOldHirerZipCode= $("#WWOldHirerZipCode").val();	//雇主邮编
	var WWOldHirerCountry=$("#country1").val();			//雇主国家
	var WWOldHirerProvince= $("#WWOldHirerProvince").val();	//雇主省/州

	/*新信息*/
	var WWNewAccountPhone=$("#WWNewAccountPhone").val().replace(/(^\s*)|(\s*$)/g, "");	//用户手机
    var WWNewAccountEmail = $("#WWNewAccountEmail").val().replace(/(^\s*)|(\s*$)/g, ""); //电子邮箱
	var WWNewAccountAddress= $("#WWNewAccountAddress").val().replace(/(^\s*)|(\s*$)/g, "");	//账户地址
	var WWNewAccountCountry=$("#country2").val();		//账户国家
	var WWNewAccountProvince=$("#WWNewAccountProvince").val().replace(/(^\s*)|(\s*$)/g, "");	//账户省/州
	var WWNewHirerName= $("#WWNewHirerName").val().replace(/(^\s*)|(\s*$)/g, "");		//雇主姓名
	var WWNewHirerAddress= $("#WWNewHirerAddress").val().replace(/(^\s*)|(\s*$)/g, "");	//雇主地址
	var WWNewHirerZipCode= $("#WWNewHirerZipCode").val().replace(/(^\s*)|(\s*$)/g, "");	//雇主邮编
	var WWNewHirerCountry=$("#country3").val();			//雇主国家
	var WWNewHirerProvince= $("#WWNewHirerProvince").val().replace(/(^\s*)|(\s*$)/g, "");	//雇主省/州
	
	/*检测输入的内容是否符合规定*/
	var WWEmailexp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!WWEmailexp.test(WWNewAccountEmail)) {
         $("#WWNewAccountEmail").parents(".form-group").addClass("has-error");
         return false;
     }

    //用户手机
    if (WWNewAccountPhone == "") {
        $("#WWNewAccountPhone").parents(".form-group").addClass("has-error");
        return false;
    }
	
    //账户地址
    if (WWNewAccountAddress == "") {
        $("#WWNewAccountAddress").parents(".form-group").addClass("has-error");
        return false;
    }
    
	//账户省/州
    if (WWNewAccountProvince == "") {
        $("#WWNewAccountProvince").parents(".form-group").addClass("has-error");
        return false;
    }
    
	//雇主姓名
    if (WWNewHirerName == "") {
        $("#WWNewHirerName").parents(".form-group").addClass("has-error");
        return false;
    }
    
	//雇主地址
    if (WWNewHirerAddress == "") {
        $("#WWNewHirerAddress").parents(".form-group").addClass("has-error");
        return false;
    }

	//雇主邮编
    if (WWNewHirerZipCode == "") {
        $("#WWNewHirerZipCode").parents(".form-group").addClass("has-error");
        return false;
    }
    
	//雇主省/州
    if (WWNewHirerProvince == "") {
        $("#WWNewHirerProvince").parents(".form-group").addClass("has-error");
        return false;
    }
	
	/*检测内容是否发生了变化*/
	if(WWOldAccountPhone!=WWNewAccountPhone){
		return true;
	}
	
	if(WWOldAccountEmail!=WWNewAccountEmail){
		return true;
	}
	
	if(WWOldAccountAddress!=WWNewAccountAddress){
		return true;
	}
	
	if(WWOldAccountCountry!=WWNewAccountCountry){
		return true;
	}
	
	if(WWOldAccountProvince!=WWNewAccountProvince){
		return true;
	}
	
	if(WWOldHirerName!=WWNewHirerName){
		return true;
	}
	
	if(WWOldHirerAddress!=WWNewHirerAddress){
		return true;
	}
	
	if(WWOldHirerZipCode!=WWNewHirerZipCode){
		return true;
	}
	
	if(WWOldHirerCountry!=WWNewHirerCountry){
		return true;
	}
	
	if(WWOldHirerProvince!=WWNewHirerProvince){
		return true;
	}
	
	return false;
}


function SubmitWebApplyData() {
    var HolderName = $("#WWAccountHolder").val(); //账户持有人姓名
    var WWTrading_account = $("#WWTrading_account").val(); //交易账号
	
	var WWAccountPhone=$("#WWNewAccountPhone").val().replace(/(^\s*)|(\s*$)/g, "");	//用户手机
    var WWAccountEmail = $("#WWNewAccountEmail").val().replace(/(^\s*)|(\s*$)/g, ""); //电子邮箱
	var WWAccountAddress= $("#WWNewAccountAddress").val().replace(/(^\s*)|(\s*$)/g, "");	//账户地址
	var WWAccountCountry=$("#country2").val();		//账户国家
	var WWAccountProvince=$("#WWNewAccountProvince").val().replace(/(^\s*)|(\s*$)/g, "");	//账户省/州
	var WWHirerName= $("#WWNewHirerName").val().replace(/(^\s*)|(\s*$)/g, "");		//雇主姓名
	var WWHirerAddress= $("#WWNewHirerAddress").val().replace(/(^\s*)|(\s*$)/g, "");	//雇主地址
	var WWHirerZipCode= $("#WWNewHirerZipCode").val().replace(/(^\s*)|(\s*$)/g, "");	//雇主邮编
	var WWHirerCountry=$("#country3").val();			//雇主国家
	var WWHirerProvince= $("#WWNewHirerProvince").val().replace(/(^\s*)|(\s*$)/g, "");	//雇主省/州
    $.ajax({
		type:"post",
		async:true,
		url:'../Agency/AccountInformationChangingForm',
		data:{
			"AccountName":HolderName,
			"AccountEmail":WWAccountEmail,"AccountCountry":WWAccountCountry,"AccountProvince":WWAccountProvince,"AccountAddress":WWAccountAddress,"AccountMobile":WWAccountPhone,
			"HirerName":WWHirerName,"HirerAddress":WWHirerAddress,"HirerZipCode":WWHirerZipCode,"HirerCountry":WWHirerCountry,"HirerProvince":WWHirerProvince
		}
	})
    // $.ajax({
        // type: "post", async: true, url: '../Agency/AccountWithdrawals',
        // data: {
            // "HolderName": HolderName, "WWTrading_account": WWTrading_account, "WWPhone": WWPhone, "WWEmail": WWEmail, "WWAddress": WWAddress, "WWCountry": WWCountry, "WWProvince": WWProvince,
            // "WWCity": WWCity, "WWZipCode": WWZipCode, "WWMoney": WWMoney, "WWAccountHolder": WWAccountHolder, "IsCloseAccountY": IsCloseAccountY, "IsCloseAccountText": IsCloseAccountText,
            // "WWBankName": WWBankName, "WWBankAccount": WWBankAccount, "AccountHolderName": AccountHolderName, "WWSWIFT_Code": WWSWIFT_Code, "WWBankAddress": WWBankAddress, "WWBankCountry": WWBankCountry,
            // "WWBankProvince": WWBankProvince, "WWBankCity": WWBankCity, "WWBankZipCode": WWBankZipCode, "RadioIsTransitBankN": RadioIsTransitBankN, "WWTransitBankName": WWTransitBankName,
            // "WWTransitBankAcoount": WWTransitBankAcoount, "WWTransitAcoountHolderName": WWTransitAcoountHolderName, "WWTransitSWIFTCode": WWTransitSWIFTCode, "WWTransitBankAddress": WWTransitBankAddress,
            // "WWTransitCountry": WWTransitCountry, "WWTransitProvince": WWTransitProvince, "WWTransitCity": WWTransitCity, "WWTransitZipCode": WWTransitZipCode, "WWMainAccountHolderName": WWMainAccountHolderName
            // //"WWMinorAccountHolderName": WWMinorAccountHolderName
        // }, success: function (data) {
            // if (data == 1) {
                // $("#modal_withdraw").modal('show');
                // setTimeout(function () { $("#modal_withdraw").modal('hide'); }, 2000);
            // } else {
                // alert("提交申请失败，请检查数据后重新提交~！");
            // }
        // }
    // })
}


