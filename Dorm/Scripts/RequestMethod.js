function RequestByPostMethod(RequestUrl, PostObject, Callback) {
    //异步传输数据功能
    $.ajax({

        type: "POST", //数据传输方式：POST/GET
        url:/*"http://192.168.2.102:3000"+*/RequestUrl, //数据提交到哪个地方
       
        data: PostObject, //需提交的数据内容

        success: function (data) {
            try {
                //数据提交成功的事件
                Callback(JSON.parse(data)); //参数Callback为回调函数
            }
            catch (err) {
                console.log(data)
                Callback(data)
            }
        },
        error: function (data) {
            //数据提交失败的事件
            alert('提交失败！');
        }
    });
}

function AxiosPost(Url, Data, Callback) {
    axios.post(Url, Data).then(function (response) {
        console.log(response.data);
        Callback(response.data);
    }).catch(function (error) {
        console.log(error);
    });
}
function AxiosGet(Url, Data, Callback) {
    axios.get(Url, Data).then(function (response) {
        console.log(response.data);
        Callback(response.data);
    }).catch(function (error) {
        console.log(error);
    });
}