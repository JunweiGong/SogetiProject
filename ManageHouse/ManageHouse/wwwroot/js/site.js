// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//go to the house-list page
$("#btn-houselist").click(function () {
    window.location.href = "/House/HouseList";
});

//sorting the table
$(function () {
    var tableObject = $('#tableSort'); 
    var tbHead = tableObject.children('thead');
    var tbHeadTh = tbHead.find('.th-head'); 
    var tbBody = tableObject.children('tbody'); 
    var tbBodyTr = tbBody.find('tr'); 

    var sortIndex = -1;

    tbHeadTh.each(function () {
        var thisIndex = tbHeadTh.index($(this));
        $(this).mouseover(function () {
            tbBodyTr.each(function () {
                var tds = $(this).find("td"); 
                $(tds[thisIndex]).addClass("hover");
            });
        }).mouseout(function () {
            tbBodyTr.each(function () {
                var tds = $(this).find("td");
                $(tds[thisIndex]).removeClass("hover");
            });
        });

        $(this).click(function () {
            var dataType = $(this).attr("type");
            checkColumnValue(thisIndex, dataType);
        });
    });

    function checkColumnValue(index, type) {
        var trsValue = new Array();
        tbBodyTr.each(function () {
            var tds = $(this).find('td');
            trsValue.push(type + ".separator" + $(tds[index]).html() + ".separator" + $(this).html());
            $(this).html("");
        });

        var len = trsValue.length;

        if (index == sortIndex) {
            trsValue.reverse();
        } else {
            for (var i = 0; i < len; i++) {
                type = trsValue[i].split(".separator")[0];
                for (var j = i + 1; j < len; j++) {
                    value1 = trsValue[i].split(".separator")[1];
                    value2 = trsValue[j].split(".separator")[1];
                    if (type == "number") {
                        value1 = value1 == "" ? 0 : value1;
                        value2 = value2 == "" ? 0 : value2;
                        if (parseFloat(value1) > parseFloat(value2)) {
                            var temp = trsValue[j];
                            trsValue[j] = trsValue[i];
                            trsValue[i] = temp;
                        }
                    } else {
                        if (value1.localeCompare(value2) > 0) {
                            var temp = trsValue[j];
                            trsValue[j] = trsValue[i];
                            trsValue[i] = temp;
                        }
                    }
                }
            }
        }

        for (var i = 0; i < len; i++) {
            $("tbody tr:eq(" + i + ")").html(trsValue[i].split(".separator")[2]);
        }

        sortIndex = index;
    }
}); // end of sorting the table


//search function for house list
function onSearch(obj) {
    setTimeout(function () {
        var storeId = document.getElementById('tableSort');
        var rowsLength = storeId.rows.length;
        var key = obj.value.toLowerCase();
        for (var i = 1; i < rowsLength; i++) {
            var searchText1 = storeId.rows[i].cells[0].innerHTML.toLowerCase();
            var searchText2 = storeId.rows[i].cells[1].innerHTML.toLowerCase();
           
            if (searchText1.match(key) || searchText2.match(key)) {
                storeId.rows[i].style.display = '';
            } else {
                storeId.rows[i].style.display = 'none';
            }
        }

    }, 200);
} //end of search function

// paging the table
var pageSize = 5;   
var curPage = 0;      
var lastPage;       
var direct = 0;       
var len;            
var page;            
var begin;
var end;

$(document).ready(function display() {
    len = $("#tableSort tr").length - 1;    
    page = len % pageSize == 0 ? len / pageSize : Math.floor(len / pageSize) + 1;
    curPage = 1;   
    displayPage(1);

    document.getElementById("btn0").innerHTML = "Current page: " + curPage + "/" + page;    
    document.getElementById("sjzl").innerHTML = "  In total: " + len + " objects";        
    document.getElementById("pageSize").value = pageSize;

    $("#btn1").click(function firstPage() {    
        curPage = 1;
        direct = 0;
        displayPage();
    });
    $("#btn2").click(function frontPage() {    
        direct = -1;
        displayPage();
    });
    $("#btn3").click(function nextPage() {    
        direct = 1;
        displayPage();
    });
    $("#btn4").click(function lastPage() {    
        curPage = page;
        direct = 0;
        displayPage();
    });
    $("#btn5").click(function changePage() {    
        curPage = document.getElementById("changePage").value * 1;
        if (!/^[1-9]\d*$/.test(curPage)) {
            alert("Please input positive integer");
            return;
        }
        if (curPage > page) {
            alert("Exceed pages");
            return;
        }
        direct = 0;
        displayPage();
    });

    $('#pageSize').change(function setPageSize() {
        pageSize = document.getElementById("pageSize").value;    
        if (!/^[1-9]\d*$/.test(pageSize)) {
            alert("Exceed pages");
            return;
        }
        len = $("#tableSort tr").length - 1;
        page = len % pageSize == 0 ? len / pageSize : Math.floor(len / pageSize) + 1;
        curPage = 1;       
        direct = 0;       
        displayPage();
    });   
});

function displayPage() {
    if (curPage <= 1 && direct == -1) {
        direct = 0;
        alert("It is the first page.");
        return;
    } else if (curPage >= page && direct == 1) {
        direct = 0;
        alert("It is the last page.");
        return;
    }

    lastPage = curPage;

    if (len > pageSize) {
        curPage = ((curPage + direct + len) % len);
    } else {
        curPage = 1;
    }

    document.getElementById("btn0").innerHTML = "Current page: " + curPage + "/" + page;    

    begin = (curPage - 1) * pageSize + 1;
    end = begin + 1 * pageSize - 1;  

    if (end > len) end = len;
    $("#tableSort tr").hide();    
    $("#tableSort tr").each(function (i) {    
        if ((i >= begin && i <= end) || i == 0)
            $(this).show();
    });
} //end of paging the table








