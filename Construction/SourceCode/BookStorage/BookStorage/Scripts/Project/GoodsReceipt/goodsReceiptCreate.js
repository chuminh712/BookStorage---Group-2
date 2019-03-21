
$("#addButton").on('click', function () {
    AddRow();
});

$("#bookID").on("change", function () {
    var id = $("#bookID").val();
    $.ajax({
        url: "/GoodsReceipt/GetBookPrice",
        type: "post",
        data: { id: id },
        success: function (response) {
            $("#price").val(response);
        }
    });
});

$("#realQuantity").on("change", function () {
    var id = $("#bookID").val();
    var realQuantity = $("#realQuantity").val();
    $.ajax({
        url: "/GoodsReceipt/GetBookPrice",
        type: "post",
        data: { id: id },
        success: function (response) {
            $("#bookTotalPrice").val(response * realQuantity);
        }
    });
});


function AddRow() {
    var selectedBook = GetSelectedBook();
    var index = $("#goodsReceiptInfoTable").children("tr").length; 
    var sl = index;
    var indexCell = "<td style='display:none'> <input type='hidden' id='index" + index + "' name='GoodsReceiptInfo.index' value='" + index + "'/> </td>";
    var serialCell = "<td>" + (++sl) + "</td>";
    var bookIDCell = "<td> <input type='hidden' id='bookID" + index + "' name='GoodsReceiptInfo[" + index + "].bookID' value='" + selectedBook.bookID + "' />" + selectedBook.bookID + " </td>";
    var receiptQuantityCell = "<td> <input type='hidden' id='receiptQuantity" + index + "' name='GoodsReceiptInfo[" + index + "].receiptQuantity' value='" + selectedBook.receiptQuantity + "' />" + selectedBook.receiptQuantity + " </td>";
    var realQuantityCell = "<td> <input type='hidden' id='realQuantity" + index + "' name='GoodsReceiptInfo[" + index + "].realQuantity' value='" + selectedBook.realQuantity + "' />" + selectedBook.realQuantity + " </td>";
    var priceCell = "<td>" + selectedBook.price + "</td>";
    var totalPriceCell = "<td class='total'> <input type='hidden' id='bookTotalPrice" + index + "' name='GoodsReceiptInfo[" + index + "].bookTotalPrice' value='" + selectedBook.bookTotalPrice + "' />" + selectedBook.bookTotalPrice + " </td >";
    var actionCell = "<td>" + "<input type='button' class='btn btn-danger' value='Xóa' onclick='getDeleteId(" + index + ")' id='" + index + "'/></td>";
    var createNewRow = "<tr id='delRow_" + index + "'> " + indexCell + serialCell + bookIDCell + receiptQuantityCell + realQuantityCell + priceCell + totalPriceCell + actionCell + " </tr>";

    $("#goodsReceiptInfoTable").append(createNewRow);
    $("#bookID").val("");
    $("#receiptQuantity").val("");
    $("#realQuantity").val("");
    $("#price").val("");
    $("#bookTotalPrice").val("");
    receiptTotalPrice();
}

function GetSelectedBook() {

    var bookID = $("#bookID").val();
    var receiptQuantity = $("#receiptQuantity").val();
    var realQuantity = $("#realQuantity").val();
    var price = $("#price").val();
    var bookTotalPrice = price * realQuantity;
    var Item = {
        "bookID": bookID,
        "receiptQuantity": receiptQuantity,
        "realQuantity": realQuantity,
        "price": price,
        "bookTotalPrice": bookTotalPrice
    };
    return Item;
}

var getDeleteId = function (id) {
    $("#delRow_" + id).remove();
};

function receiptTotalPrice() {
    var sumOfTotal = 0;
    if ($("#goodsReceiptInfoTable").children("tr").length == 0) {
        $("#receiptTotalPrice").val(0);
    }
    else {
        $("#goodsReceiptInfoTable tr ").each(function (index, value) {
            var total = parseFloat((document.getElementById("bookTotalPrice" + index).value));
            sumOfTotal = sumOfTotal + total;
            $("#receiptTotalPrice").val(sumOfTotal);
        });
    }
}