
    //呼叫API方法

function POSTMethod(url, datas, success) {

    $.ajax({
        type: "POST",
        url: url,
        data: datas,
        success: success
    })
}


function PageList(page, elementli, totalpage)
{

    let first = page - 2;
    let last = page + 2;

    elementli.css("display", "none");

    if (first < 1) {
        for (i = 1; i < 5; i++)
            elementli.eq(i).css("display", "inline");
    }

    if (last > totalpage) {
        for (i = totalpage - 3; i < totalpage + 1; i++)
            elementli.eq(i).css("display", "inline");
    }

    if (page > 2 && page < totalpage - 1) {
        for (i = first; i < last; i++)
            elementli.eq(i).css("display", "inline");
    }

    elementli.eq(0).css("display", "inline");
    elementli.eq(totalpage + 1).css("display", "inline");

}