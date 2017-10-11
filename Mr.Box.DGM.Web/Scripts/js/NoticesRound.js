function NoticesHome() {
    setTimeout(function () {
        $.ajax({
            type: "POST", async: true, url: '../AboutUs/GetNotices',
            success: function (data) {
                if (data != "") {
                    var news = eval(data);
                    var html = "<ul>";
                    for (var i = 0; i < news.length; i++) {
                        // html += "<li><a href='javascript:newsDetail(" + news[i].DGMNewsId + ")'><img src='../Images/img/ggnew2.png'>" + news[i].NewsTitle + "</a></li>";
                        html += "<li><a target='_blank' href='../AboutUs/Details?id=" + news[i].DGMNewsId + "'><img src='../Images/img/ggnew2.png'>" + news[i].NewsTitle + "</a></li>";
                    }
                    html += "</ul>";
                    $(".Notice").append(html);
                    $(".Notice").css("display", "block");
                    $(".Notice").Scroll({ line: 1, speed: 500, timer: 3000 });
                }
            }
        })
    }, 1000);
}