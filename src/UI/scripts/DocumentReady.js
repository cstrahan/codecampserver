Date.format = 'mm/dd/yyyy';

$(function() {
    $('.date-pick').datePicker({ startDate: '01/01/2009' });
});

$(document).ready(function() {
    $('.datatable').dataTable();
});

$(function() {
    $(':input[type=\'text\']:visible:enabled:first').focus();
});
