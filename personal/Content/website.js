var chatHub;

function startSignalR() {
    chatHub = $.connection.chatHub;

    chatHub.client.sendMessage = function (name, message) {
        $("<tr>").append(
            $("<td>").text(name + ":"),
            $("<td>").text(message)
        ).appendTo("#messages");
    };

    chatHub.client.userConnected = function (id, name) {
        $("<tr>")
            .attr("id", "user" + id)
            .append(
                $("<td>").text(name)
            ).appendTo("#users");
    };

    chatHub.client.userDisconnected = function (id) {
        $("#user" + id).remove();
    };

    $.connection.hub.start().done(function () {
        $("#send").click(function () {
            chatHub.server.writeMessage($("#name").val(), $("#message").val());
            $("#message").val("").focus();
        });
    });

    $("#logoff").click(function () {
        $.connection.hub.stop();
        chatHub = null;
        window.setTimeout(function () {
            $("#logoffForm").submit();
        }, 500);
    });

    $("#name").focus();
}