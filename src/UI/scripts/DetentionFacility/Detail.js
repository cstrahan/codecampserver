function commonTemplate(item) 
{
	var option = '<option value=\'' + item.Value + '\' >' + item.Text + '</option>';

	return option
};

function commonMatch(selectedValue) {
    return this.When == selectedValue; 
};

var option = "<option value='' selected='selected'>-- Filter --</option>";

function AddFirstOptionAndSelectIt(e)
{
	$(e).prepend(option);
    $(e).find("option:first")[0].selected = true;
}

$(document).ready(function()
{
	$(".occupantDetails").hide();
	$("#ShowOccupantDetails").click(function(){
		$(".occupantDetails").toggle();
	});
	
    $("#selects select").change(function(){
       $(this).parent().nextAll().children("select").prepend(option);
       $(this).parent().nextAll().children("select").find("option:first")[0].selected = true;
       $(this).parent().nextAll().children("select").attr("disabled", "disabled");
    });			
				
    $("#by-floor").cascade("#by-building", {
        ajax: {url: '/detentionfacility/floors'},				
        template: commonTemplate,
        match: commonMatch
    }).bind("loaded.cascade", function(e, target) {
       AddFirstOptionAndSelectIt(this); 			
    });
	
    $("#by-section").cascade("#by-floor", {
        ajax: {url: '/detentionfacility/sections'},				
        template: commonTemplate,
        match: commonMatch
    }).bind("loaded.cascade", function(e, target) {
       AddFirstOptionAndSelectIt(this);
    });
	
    $("#by-sectionunit").cascade("#by-section", {
        ajax: {url: '/detentionfacility/sectionunits'},				
        template: commonTemplate,
        match: commonMatch
    }).bind("loaded.cascade", function(e, target) {
        AddFirstOptionAndSelectIt(this);
    });
	
    $("#by-room").cascade("#by-sectionunit", {
        ajax: {url: '/detentionfacility/rooms'},				
        template: commonTemplate,
        match: commonMatch
    }).bind("loaded.cascade", function(e, target) {
        AddFirstOptionAndSelectIt(this);
    });
});