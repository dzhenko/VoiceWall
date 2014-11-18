$(function () {
    var chat = $.connection.chat;

    chat.client.addMessage = function (name, message) {
        // Add the message to the page.
        $('#all-messages').prepend('<div class="alert alert-dismissable alert-success"><button type="button" class="close" data-dismiss="alert">×</button><h4>' + htmlEncode(name) + '</h4><p>' + htmlEncode(message) + '</p></div>');
    };

    $.connection.hub.start().done(function () {
        $("#send-message-btn").click(function () {
            var name = window.currentUsernameName;
            var message = $("#send-message-text").val();
            $("#send-message-text").val("").focus();

            chat.server.send(name, message);
        });
    });

    var entityMap = {
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        '"': '&quot;',
        "'": '&#39;',
        "/": '&#x2F;'
    };

    function htmlEncode(string) {
        return String(string).replace(/[&<>"'\/]/g, function (s) {
            return entityMap[s];
        });
    };
});