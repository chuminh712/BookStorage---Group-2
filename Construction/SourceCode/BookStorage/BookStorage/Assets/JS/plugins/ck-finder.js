        $('#btnSelectImage').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#txtImage').val(url);
            };
            finder.popup();
        })