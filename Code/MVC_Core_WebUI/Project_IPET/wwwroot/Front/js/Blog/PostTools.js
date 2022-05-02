
    //呼叫API方法

function POSTMethod(url, datas, success) {

    $.ajax({
        type: "POST",
        url: url,
        data: datas,
        success: success
    })
}