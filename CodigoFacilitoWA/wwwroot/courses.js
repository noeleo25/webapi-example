const uri = "api/courses";

$(document).ready(function () {
    getCourses();
})

function getCourses() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#courses");
            $(tBody).empty();

            $.each(data, function (k, item) {
                const tr = $("<tr> </tr>")
                    .append($("<td></td>").text(item.courseId))
                    .append($("<td></td>").text(item.name))
                    .append($("<td></td>").text(item.duration))
                    .append($("<td></td>").text(item.instructorName))
                    .append($("<td></td>").text(item.isActive ? 'Activo' : 'Inactivo'))
                    .append(
                        $("<td></td>").append(
                            $('<button class="btn btn-sm btn-danger"> Delete </button> ')
                                .on("click", function () {
                                    deleteCourse(item.courseId);
                                })
                        )
                    );
                tr.appendTo(tBody);
            });
        }
    })
}

function addCourse() {
    const course = {
        name: $("#add-course-name").val(),
        duration: $("#add-course-dur").val(),
        instructorName: $("#add-course-inst").val(),
        isActive: $("#add-course-active").is(':checked'),
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(course),
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Error on Ajax POST. Error: " + errorThrown);
        },
        success: function (result) {
            getCourses();
            $("#add-course-name").val("");
            $("#add-course-dur").val("");
            $("#add-course-inst").val("");
        }
    });
}

function deleteCourse(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getCourses();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Error on Ajax DELETE. Error: " + errorThrown);
        },
    })
}