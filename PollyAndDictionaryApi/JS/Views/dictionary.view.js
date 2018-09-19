
(function () {
    $('body > div.container.body-content > div > button').bind("click", function () {

        var word = $('body > div.container.body-content > div > input').val();
        $.get({
            url: '/Home/Query?word=' + word
        }).done(function (result) {
            var data = result.Data[0];

            $('#dictionary-content > div.col.justify-content-start > div > h5').html(data.Word);

            $('#dictionary-content > div.col.justify-content-center > div > div > p').html('');

            $.each(data.LexicalEntries, function (index1, entries) {
                $.each(entries, function (index2, entry) {
                    $.each(entry, function (index3, sens) {
                        $.each(sens.Senses, function (index4, sen) {
                            $.each(sen.Definitions, function (index5, definition) {
                                $('#dictionary-content > div.col.justify-content-center > div > div > p').append(definition).append('<br />').append('<p />');
                            });
                        });

                    });
                });
            });

            $('#dictionary-content > div.col.justify-content-end > div > div > p').html('');

            $.each(data.LexicalEntries, function (index1, entries) {
                $.each(entries, function (index2, entry) {
                    $.each(entry, function (index3, sens) {
                        $.each(sens.Senses, function (index4, sen) {
                            $.each(sen.Examples, function (index5, example) {
                                $('#dictionary-content > div.col.justify-content-end > div > div > p').append(example.Text).append('<br />').append('<p />');
                            });
                        });

                    });
                });
            });

            console.log(result);
        }).fail(function (xhrexception) {
            alert('something went wrong! ' + JSON.stringify(xhrexception));
        });
    });
    $('#dictionary-container > div.container > div > div.col-sm > button').bind("click", function () {
        var url = "speech.mp3?v=" + new Date().getTime();
        var audio = new Audio(url);
        audio.load();
        audio.play();
    })
})();