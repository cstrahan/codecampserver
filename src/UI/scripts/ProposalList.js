/// <reference path="jquery-1.2.6-vsdoc.js" />

$(document).ready(function() {
    $('.proposal:even').addClass("proposal-alternating");
    $('.proposal').corners();
    $('a.vote').click(function() {
        //console.log("a.star click");
        var $link = $(this);
        $('body').css({ 'cursor': 'wait' });


        $.get($link.attr("href"), null, function(e) {

            $link.after($("<span>&nbsp;</span>").addClass("star")).hide();
            $('body').css({ 'cursor': 'auto' });
        })


        return false;
    });

});