function initialize() {
  $(function() {
    //all hover and click logic for buttons
    $("button:not(.ui-state-disabled)")
      .hover(
	      function() {
	        $(this).addClass("ui-state-hover");
	      },
	      function() {
	        $(this).removeClass("ui-state-hover");
	      }
      )
  });

  // DatePicker
  $(".datepicker").datepicker({ showOn: 'both', buttonImage: '/images/icons/calendar.png', buttonImageOnly: true });
}