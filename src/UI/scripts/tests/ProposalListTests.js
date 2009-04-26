/// <reference path="../jquery-1.2.6-vsdoc.js" />
/// <reference path="testrunner.js" />
var getOriginal = $.get;
var getStub = function(url, data, callback) {
    
    ok(true, "executed get method");
    callback();
};
var setup = function() {
    console.log("setup");
    $.get = getStub;
}

var teardown = function(){
    console.log("teardown");
    
}
var doTest = function(message, test) {
    setup();
    test(message, test);
    teardown();
}
$(function() {
    module("when submitting a vote request",
    {
        setup: function() {
            $.get = getStub;
        },
        teardown: function() {
            $.get = getOriginal;
        }
        
    });
    
    test("after sending request should change the class to complete", function() {
        console.log($("#vote1").attr("class"));
        
        $("#vote1").click();
        console.log("assertions, " + $("#vote1").attr("class"));
        ok($("#vote1").hasClass("checked"), $("#vote1").attr("class"));
        ok($("#vote1").hasClass("star") == false, "should not have the star class name");
        
    });
   

    
});

