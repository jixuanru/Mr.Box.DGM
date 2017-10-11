//转换时间 字符串→Y/M/D h:m:s
function ConvertDate(stringDate) {
    var dateTime = "";
    var Y = stringDate.substr(0, 4);
    var M = stringDate.substr(4, 2);
    var D = stringDate.substr(6, 2);
    var h = stringDate.substr(8, 2);
    var m = stringDate.substr(10, 2);
    var s = stringDate.substr(12, 2);
    dateTime = Y + "-" + M + "-" + D + " " + h + ":" + m + ":" + s;
    // alert(dateTime);
    return dateTime;
}


//去除所有的空格
function delHtmlTag(str) {
    var strHtml = str.replace(/<\/?[^,text:"]*,text:"/gim, "");//去掉所有的html标记
    var result = strHtml.replace(/(^\s+)|(\s+$)/g, "");//去掉前后空格
    return result.replace(/\s/g, "");//去除文章中间空格
}
//验证邮箱
function CheckMail(mail) {
    var re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;
    if (!re.test(mail)) {
        //$.ligerDialog.warn("邮箱格式错误!");
        return false;
    } else {
        return true;
    }
}



function GetArgsFromHref(sHref, sArgName) {
    var args = sHref.split("?");
    var retval = "";

    if (args[0] == sHref) /*参数为空*/ {
        return retval; /*无需做任何处理*/
    }
    var str = args[1];
    args = str.split("&");
    for (var i = 0; i < args.length; i++) {
        str = args[i];
        var arg = str.split("=");
        if (arg.length <= 1) continue;
        if (arg[0] == sArgName) retval = arg[1];
    }
    return retval;
}

