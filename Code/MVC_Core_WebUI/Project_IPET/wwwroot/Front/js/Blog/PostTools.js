
    /*呼叫API方法*/

    function POSTMethod(url, data, success)
    {
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            success: success,
            error: function (thrownError) {
                console.log(thrownError);
            }
        })
    }

