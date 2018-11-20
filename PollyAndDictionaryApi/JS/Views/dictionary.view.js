(function () {
    $('#dictionary-container').hide();

    var consultButton = $('body > div.container.body-content > div.input-group.mb-3 > div.input-group-append > button');
    var audioButton = $('#dictionary-container > div.container > div > div.col-sm > button');
    var consultWord = $('body > div.container.body-content > div.input-group.mb-3 > input');

    var displayContent = $('#dictionary-container');
    var displayWord = $('#dictionary-content > div.col.justify-content-start > div > h5');
    var displayDefinition = $('#dictionary-content > div.col.justify-content-center > div > div > p');
    var displayExamples = $('#dictionary-content > div.col.justify-content-end > div > div > p');

    consultButton.bind("click", function () {
        $('.modal').modal('show');

        var word = consultWord.val();

        if (word.length == 0) {
            return;
        }

        $.get({
            url: '/Home/Query?word=' + word
        }).done(function (result) {
            var data = result.Data[0];

            displayWord.html(data.Word);

            displayDefinition.html('');

            $.each(data.LexicalEntries, function (index1, entries) {
                $.each(entries, function (index2, entry) {
                    $.each(entry, function (index3, sens) {
                        $.each(sens.Senses, function (index4, sen) {
                            $.each(sen.Definitions, function (index5, definition) {
                                displayDefinition.append(definition).append('<br />').append('<p />');
                            });
                        });
                    });
                });
            });

            displayExamples.html('');

            $.each(data.LexicalEntries, function (index1, entries) {
                $.each(entries, function (index2, entry) {
                    $.each(entry, function (index3, sens) {
                        $.each(sens.Senses, function (index4, sen) {
                            $.each(sen.Examples, function (index5, example) {
                                displayExamples.append(example.Text).append('<br />').append('<p />');
                            });
                        });
                    });
                });
            });
            displayContent.show();
            $('.modal').modal('hide');
            console.log(result);
        }).fail(function (xhrexception) {
            displayContent.hide();
            $('.modal').modal('hide');
            alert('something went wrong! ' + JSON.stringify(xhrexception.statusText));
        });
    });

    audioButton.bind("click", function () {
        var url = "speech.mp3?v=" + new Date().getTime();
        var audio = new Audio(url);
        audio.load();
        audio.play();
    });
})();