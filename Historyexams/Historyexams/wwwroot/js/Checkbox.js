$(document).ready(function () {


    //chọn nhều bản ghi cùng lúc
    $('body').on('change', '#SelectAll', function () {
        var checkStatus = this.checked;
        var checkbox = $(this).parents('.card-body').find('tr td input:checkbox');
        checkbox.each(function () {
            this.checked = checkStatus;
            if (this.checked) {
                checkbox.attr('selected', 'checked');
            } else {
                checkbox.attr('selected', '');
            }
        });
    });

    //xoá nhiều bản ghi cùng lúc
    $('body').on('click', '#BtnDeleteAll', function (e) {
        e.preventDefault();
        var str = "";
        var checkbox = $(this).parents('.card').find('tr td input:checkbox');
        var i = 0;
        checkbox.each(function () {
            if (this.checked) {
                var _id = $(this).val();
                if (i === 0) {
                    str += _id;
                } else {
                    str += "," + _id;
                }
                i++;
            } else {
                checkbox.attr('selected', '');
            }
        });
        
        if (str.length > 0) {
            
            Swal.fire({
                title: "Bạn có muốn xóa các bản ghi này hay không?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Có",
                cancelButtonText: "Không"
            }).then((result) => {
                console.log("okeeee")
                if (result.isConfirmed) {
                    $.ajax({
                        url: baseUrl,
                        type: 'POST',
                        data: { ids: str },
                        success: function (rs) {
                            if (rs.success) {
                                Swal.fire({
                                    title: "Đã xóa!",
                                    icon: "success"
                                }).then(() => {
                                    location.reload();
                                });
                            } else {
                                Swal.fire("Đã xảy ra lỗi khi xóa các bản ghi.", "", "error");
                            }
                        },
                        error: function () {
                            Swal.fire("Đã xảy ra lỗi khi xóa các bản ghi.", "", "error");
                        }
                    });
                }
            });
        } else {
            Swal.fire('Vui lòng chọn ít nhất một bản ghi để xóa.', '', 'info');
        }
    });

    // checkbox tình trạng bật tắt
    $('body').on('click', '.btnActive', function (e) {
       
        e.preventDefault();
        var btn = $(this);
        var id = btn.data("id");
        $.ajax({
            url: activeUrl, // chay vào IActionResult IsActicve(int id)
            type: 'POST',
            data: { id: id },
            success: function (rs) {
                if (rs.success) {
                    if (rs.isActive) {
                        btn.html("<i class='fa fa-check text-success'></i>");
                    } else {
                        btn.html("<i class='fas fa-times text-danger'></i>");
                    }
                }

            }
        });
    });
});