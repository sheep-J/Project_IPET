
//呼叫API方法

function POSTMethod(url, datas, success) {

    $.ajax({
        type: "POST",
        url: url,
        data: datas,
        success: success
    })
}


//頁數陣列JS

function PageList(page, elementli, totalpage)
{

    let first = page - 2;
    let last = page + 2;

    elementli.css("display", "none");

    if (first < 1) {
        for (i = 1; i < 6; i++)
            elementli.eq(i).css("display", "inline");
    }

    if (last > totalpage) {
        for (i = totalpage - 4; i < totalpage + 1; i++)
            elementli.eq(i).css("display", "inline");
    }

    if (page > 2 && page < totalpage - 1) {
        for (i = first; i < last + 1; i++)
            elementli.eq(i).css("display", "inline");
    }

    elementli.eq(0).css("display", "inline");
    elementli.eq(totalpage + 1).css("display", "inline");

}
 //頁數陣列Html

 //< !--Page Start-- >
 //   <div class="row">
 //       <div class="col-12 m-b-50">
 //           <nav class="d-flex justify-content-end ">
 //               <ul class="pagination ">
 //                   <li class="page-item">
 //                       <a class="page-link " id="page-ctrl-previous">
 //                           <span>&laquo;</span>
 //                       </a>
 //                   </li>
 //                   @{
 //                       int pagenum = ViewBag.page;
 //                       for (int i = 1; i <= pagenum; i++)
 //                       {

 //                           <li class="page-item " style="display:none"><a class="page-link " onclick="post(@i)">@i</a></li>

 //                       }

 //                   }
 //                   <li class="page-item">
 //                       <a class="page-link " id="page-ctrl-next">
 //                           <span>&raquo;</span>
 //                       </a>
 //                   </li>
 //               </ul>
 //               <h2 id="pagenumber" style="width:100px; text-align: center;"></h2>
 //           </nav>
 //       </div>
 //   </div>
 //   <!--Page End-- >





