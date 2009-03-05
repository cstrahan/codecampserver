/*
 * File:        jquery.dataTables.js
 * Version:     1.4.0 beta 7
 * CVS:         $Id$
 * Description: Paginate, search and sort HTML tables
 * Author:      Allan Jardine (www.sprymedia.co.uk)
 * Created:     28/3/2008
 * Modified:    $Date$ by $Author$
 * Language:    Javascript
 * License:     GPL v2 or BSD 3 point style
 * Project:     Mtaala
 * Contact:     allan.jardine@sprymedia.co.uk
 * 
 * Copyright 2008-2009 Allan Jardine, all rights reserved.
 *
 * This source file is free software, under either the GPL v2 license or a
 * BSD style license, as supplied with this software.
 * 
 * This source file is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
 * or FITNESS FOR A PARTICULAR PURPOSE. See the license files for details.
 * 
 * For details pleease refer to: http://sprymedia.co.uk/article/DataTables
 */


(function($) {
	/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
	 * DataTables variables
	 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
	
	/*
	 * Variable: dataTableSettings
	 * Purpose:  Store the settings for each dataTables instance
	 * Scope:    jQuery.fn
	 */
	$.fn.dataTableSettings = new Array();
	
	/*
	 * Variable: dataTableExt
	 * Purpose:  Container for customisable parts of DataTables
	 * Scope:    jQuery.fn
	 */
	$.fn.dataTableExt = new Object();
	
	
	
	/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
	 * DataTables extensible objects
	 * 
	 * The $.fn.dataTableExt object is used to provide an area where user dfined plugins can be 
	 * added to DataTables. The following properties of the object are used:
	 *   oApi - Plug-in API functions
	 *   aTypes - Auto-detection of types
	 *   oSort - Sorting functions used by DataTables (based on the type)
	 *   oPagination - Pagination functions for different input styles
	 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
	
	/*
	 * Variable: oApi
	 * Purpose:  Container for plugin API functions
	 * Scope:    jQuery.fn.dataTableExt
	 */
	$.fn.dataTableExt.oApi = { };
	
	/*
	 * Variable: oPagination
	 * Purpose:  Container for the various type of pagination that dataTables supports
	 * Scope:    jQuery.fn.dataTableExt
	 */
	$.fn.dataTableExt.oPagination = {
		/*
		 * Variable: two_button
		 * Purpose:  Standard two button (forward/back) pagination
	 	 * Scope:    jQuery.fn.dataTableExt.oPagination
		 */
		"two_button": {
			/*
			 * Function: oPagination.two_button.fnInit
			 * Purpose:  Initalise dom elements required for pagination with forward/back buttons only
			 * Returns:  -
	 		 * Inputs:   object:oSettings - dataTables settings object
			 *           function:fnCallbackDraw - draw function which must be called on update
			 */
			"fnInit": function ( oSettings, fnCallbackDraw )
			{
				oSettings.nPrevious = document.createElement( 'div' );
				oSettings.nNext = document.createElement( 'div' );
				
				if ( oSettings.sTableId != "" )
				{
					oSettings.nPaginate.setAttribute( 'id', oSettings.sTableId+'_paginate' );
					oSettings.nPrevious.setAttribute( 'id', oSettings.sTableId+'_previous' );
					oSettings.nNext.setAttribute( 'id', oSettings.sTableId+'_next' );
				}
				
				oSettings.nPrevious.className = "paginate_disabled_previous";
				oSettings.nNext.className = "paginate_disabled_next";
				
				oSettings.nPaginate.appendChild( oSettings.nPrevious );
				oSettings.nPaginate.appendChild( oSettings.nNext );
				$(oSettings.nPaginate).insertAfter( oSettings.nTable );
				
				$(oSettings.nPrevious).click( function() {
					oSettings.iDisplayStart -= oSettings.iDisplayLength;
					
					/* Correct for underrun */
					if ( oSettings.iDisplayStart < 0 )
					{
					  oSettings.iDisplayStart = 0;
					}
					
					fnCallbackDraw( oSettings );
				} );
				
				$(oSettings.nNext).click( function() {
					/* Make sure we are not over running the display array */
					if ( oSettings.iDisplayStart + oSettings.iDisplayLength < oSettings.aiDisplay.length )
						oSettings.iDisplayStart += oSettings.iDisplayLength;
					
					fnCallbackDraw( oSettings );
				} );
			},
			
			/*
			 * Function: oPagination.two_button.fnUpdate
			 * Purpose:  Update the two button pagination at the end of the draw
			 * Returns:  -
	 		 * Inputs:   object:oSettings - dataTables settings object
			 *           function:fnCallbackDraw - draw function which must be called on update
			 */
			"fnUpdate": function ( oSettings, fnCallbackDraw )
			{
				oSettings.nPrevious.className = 
					( oSettings.iDisplayStart == 0 ) ? 
					"paginate_disabled_previous" : "paginate_enabled_previous";
				
				oSettings.nNext.className = 
					( oSettings.iDisplayEnd == oSettings.aiDisplay.length ) ? 
					"paginate_disabled_next" : "paginate_enabled_next";
			}
		},
		
		
		/*
		 * Variable: iFullNumbersShowPages
		 * Purpose:  Change the number of pages which can be seen
	 	 * Scope:    jQuery.fn.dataTableExt.oPagination
		 */
		"iFullNumbersShowPages": 5,
		
		/*
		 * Variable: full_numbers
		 * Purpose:  Full numbers pagination
	 	 * Scope:    jQuery.fn.dataTableExt.oPagination
		 */
		"full_numbers": {
			/*
			 * Function: oPagination.full_numbers.fnInit
			 * Purpose:  Initalise dom elements required for pagination with a list of the pages
			 * Returns:  -
	 		 * Inputs:   object:oSettings - dataTables settings object
			 *           function:fnCallbackDraw - draw function which must be called on update
			 */
			"fnInit": function ( oSettings, fnCallbackDraw )
			{
				var nFirst = document.createElement( 'span' );
				var nPrevious = document.createElement( 'span' );
				var nList = document.createElement( 'span' );
				var nNext = document.createElement( 'span' );
				var nLast = document.createElement( 'span' );
				
				nFirst.appendChild( document.createTextNode( oSettings.oLanguage.oPaginate.sFirst ) );
				nPrevious.appendChild( document.createTextNode( oSettings.oLanguage.oPaginate.sPrevious ) );
				nNext.appendChild( document.createTextNode( oSettings.oLanguage.oPaginate.sNext ) );
				nLast.appendChild( document.createTextNode( oSettings.oLanguage.oPaginate.sLast ) );
				
				nFirst.className = "paginate_button first";
				nPrevious.className = "paginate_button previous";
				nNext.className="paginate_button next";
				nLast.className = "paginate_button last";
				
				oSettings.nPaginate.appendChild( nFirst );
				oSettings.nPaginate.appendChild( nPrevious );
				oSettings.nPaginate.appendChild( nList );
				oSettings.nPaginate.appendChild( nNext );
				oSettings.nPaginate.appendChild( nLast );
				
				$(nFirst).click( function () {
					oSettings.iDisplayStart = 0;
					fnCallbackDraw( oSettings );
				} );
				
				$(nPrevious).click( function() {
					oSettings.iDisplayStart -= oSettings.iDisplayLength;
					
					/* Correct for underrun */
					if ( oSettings.iDisplayStart < 0 )
					  oSettings.iDisplayStart = 0;
					
					fnCallbackDraw( oSettings );
				} );
				
				$(nNext).click( function() {
					/* Make sure we are not over running the display array */
					if ( oSettings.iDisplayStart + oSettings.iDisplayLength < oSettings.aiDisplay.length )
						oSettings.iDisplayStart += oSettings.iDisplayLength;
					
					fnCallbackDraw( oSettings );
				} );
				
				$(nLast).click( function() {
					var iPages = parseInt( (oSettings.aiDisplay.length-1) / oSettings.iDisplayLength ) + 1;
					oSettings.iDisplayStart = (iPages-1) * oSettings.iDisplayLength;
					
					fnCallbackDraw( oSettings );
				} );
				
				oSettings.nPaginateList = nList;
			},
			
			/*
			 * Function: oPagination.full_numbers.fnUpdate
			 * Purpose:  Update the list of page buttons shows
			 * Returns:  -
	 		 * Inputs:   object:oSettings - dataTables settings object
			 *           function:fnCallbackDraw - draw function which must be called on update
			 */
			"fnUpdate": function ( oSettings, fnCallbackDraw )
			{
				var iPageCount = jQuery.fn.dataTableExt.oPagination.iFullNumbersShowPages;
				var iPageCountHalf = Math.floor(iPageCount / 2);
				var iPages = parseInt( (oSettings.aiDisplay.length-1) / oSettings.iDisplayLength ) + 1;
				var iCurrentPage = parseInt( oSettings.iDisplayStart / oSettings.iDisplayLength ) + 1;
				var sList = "";
				var iStartButton;
				var iEndButton;
				
				if (iPages < iPageCount)
				{
					iStartButton = 1;
					iEndButton = iPages;
				}
				else
				{
					if (iCurrentPage <= iPageCountHalf)
					{
						iStartButton = 1;
						iEndButton = iPageCount
					}
					else
					{
						if (iCurrentPage >= (iPages - iPageCountHalf))
						{
							iStartButton = iPages - iPageCount + 1;
							iEndButton = iPages
						}
						else
						{
							iStartButton = iCurrentPage - Math.ceil(iPageCount / 2) + 1;
							iEndButton = iStartButton + iPageCount - 1;
						}
					}
				}
				
				for ( var i=iStartButton ; i<=iEndButton ; i++ )
				{
					if ( iCurrentPage != i )
						sList += '<span class="paginate_button">'+i+'</span>';
					else
						sList += '<span class="paginate_active">'+i+'</span>';
				}
				
				oSettings.nPaginateList.innerHTML = sList;
				
				$('span', oSettings.nPaginateList).click( function() {
					var iTarget = (this.innerHTML * 1) - 1;
					oSettings.iDisplayStart = iTarget * oSettings.iDisplayLength;
					
					fnCallbackDraw( oSettings );
				} );
			}
		}
	}
	
	/*
	 * Variable: oSort
	 * Purpose:  Wrapper for the sorting functions that can be used in DataTables
	 * Scope:    jQuery.fn.dataTableExt
	 * Notes:    The functions provided in this object are basically standard javascript sort
	 *   functions - they expect two inputs which they then compare and then return a priority
	 *   result. For each sort method added, two functions need to be defined, an ascending sort and
	 *   a descending sort.
	 */
	$.fn.dataTableExt.oSort = {
		/*
		 * text sorting
		 */
		"string-asc": function ( a, b )
		{
			var x = a.toLowerCase();
			var y = b.toLowerCase();
			return ((x < y) ? -1 : ((x > y) ? 1 : 0));
		},
		
		"string-desc": function ( a, b )
		{
			var x = a.toLowerCase();
			var y = b.toLowerCase();
			return ((x < y) ? 1 : ((x > y) ? -1 : 0));
		},
		
		
		/*
		 * html sorting (ignore html tags)
		 */
		"html-asc": function ( a, b )
		{
			var x = a.replace( /<.*?>/g, "" ).toLowerCase();
			var y = b.replace( /<.*?>/g, "" ).toLowerCase();
			return ((x < y) ? -1 : ((x > y) ? 1 : 0));
		},
		
		"html-desc": function ( a, b )
		{
			var x = a.replace( /<.*?>/g, "" ).toLowerCase();
			var y = b.replace( /<.*?>/g, "" ).toLowerCase();
			return ((x < y) ? 1 : ((x > y) ? -1 : 0));
		},
		
		
		/*
		 * date sorting
		 */
		"date-asc": function ( a, b )
		{
			var x = Date.parse( a );
			var y = Date.parse( b );
			
			if ( isNaN( x ) )
			{
    		x = Date.parse( "01/01/1970 00:00:00" );
			}
			if ( isNaN( y ) )
			{
				y =	Date.parse( "01/01/1970 00:00:00" );
			}
			
			return x - y;
		},
		
		"date-desc": function ( a, b )
		{
			var x = Date.parse( a );
			var y = Date.parse( b );
			
			if ( isNaN( x ) )
			{
    		x = Date.parse( "01/01/1970 00:00:00" );
			}
			if ( isNaN( y ) )
			{
				y =	Date.parse( "01/01/1970 00:00:00" );
			}
			
			return y - x;
		},
		
		
		/*
		 * numerical sorting
		 */
		"numeric-asc": function ( a, b )
		{
			var x = a == "-" ? 0 : a;
			var y = b == "-" ? 0 : b;
			return x - y;
		},
		
		"numeric-desc": function ( a, b )
		{
			var x = a == "-" ? 0 : a;
			var y = b == "-" ? 0 : b;
			return y - x;
		}
	};
	
	
	/*
	 * Variable: aTypes
	 * Purpose:  Container for the various type of type detection that dataTables supports
	 * Scope:    jQuery.fn.dataTableExt
	 * Notes:    The functions in this array are expected to parse a string to see if it is a data
	 *   type that it recognises. If so then the function should return the name of the type (a
	 *   corresponding sort function should be defined!), if the type is not recognised then the
	 *   function should return null such that the parser and move on to check the next type.
	 *   Note that ordering is important in this array - the functions are processed linearly,
	 *   starting at index 0.
	 */
	$.fn.dataTableExt.aTypes = new Array (
		/*
		 * Function: -
		 * Purpose:  Check to see if a string is numeric
		 * Returns:  string:'numeric' or null
		 * Inputs:   string:sText - string to check
		 */
		function ( sData )
		{
			var sValidChars = "0123456789.-";
			var Char;
			
			for ( i=0 ; i<sData.length ; i++ ) 
			{ 
				Char = sData.charAt(i); 
				if (sValidChars.indexOf(Char) == -1) 
				{
					return null;
				}
			}
			
			return 'numeric';
		},
		
		/*
		 * Function: -
		 * Purpose:  Check to see if a string is actually a formatted date
		 * Returns:  string:'date' or null
		 * Inputs:   string:sText - string to check
		 */
		function ( sData )
		{
			if ( ! isNaN(Date.parse(sData) ) )
			{
				return 'date';
			}
			return null;
		}
	);
	
	
	/*
	 * Variable: _oExternConfig
	 * Purpose:  Store information for DataTables to access globally about other instances
	 * Scope:    jQuery.fn.dataTableExt
	 */
	$.fn.dataTableExt._oExternConfig = {
		/* int:iNextUnique - next unique number for an instance */
		"iNextUnique": 0
	};
	
	
	/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
	 * DataTables prototype
	 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
	
	/*
	 * Function: dataTable
	 * Purpose:  DataTables information
	 * Returns:  -
	 * Inputs:   object:oInit - initalisation options for the table
	 */
	$.fn.dataTable = function( oInit )
	{
		/*
		 * Variable: _aoSettings
		 * Purpose:  Easy reference to data table settings
		 * Scope:    jQuery.dataTable
		 */
		var _aoSettings = $.fn.dataTableSettings;
		
		/*
		 * Function: classSettings
		 * Purpose:  Settings container function for all 'class' properties which are required
		 *   by dataTables
		 * Returns:  -
		 * Inputs:   -
		 */
		function classSettings ()
		{
			/*
			 * Variable: sInstance
			 * Purpose:  Unique idendifier for each instance of the DataTables object
			 * Scope:    jQuery.dataTable.classSettings 
			 */
			this.sInstance = null;
			
			/*
			 * Variable: oFeatures
			 * Purpose:  Indicate the enablement of key dataTable features
			 * Scope:    jQuery.dataTable.classSettings 
			 */
			this.oFeatures = {
				"bPaginate": true,
				"bLengthChange": true,
				"bFilter": true,
				"bSort": true,
				"bInfo": true,
				"bAutoWidth": true,
				"bProcessing": false,
				"bSortClasses": true,
				"bStateSave": false
			};
			
			/*
			 * Variable: oLanguage
			 * Purpose:  Store the language strings used by dataTables
			 * Scope:    jQuery.dataTable.classSettings
			 * Notes:    The words in the format _VAR_ are variables which are dynamically replaced
			 *   by javascript
			 */
			this.oLanguage = {
				"sProcessing": "Processing...",
				"sLengthMenu": "Show _MENU_ entries",
				"sZeroRecords": "No matching records found",
				"sInfo": "Showing _START_ to _END_ of _TOTAL_ entries",
				"sInfoEmpty": "Showing 0 to 0 of 0 entries",
				"sInfoFiltered": "(filtered from _MAX_ total entries)",
				"sInfoPostFix": "",
				"sSearch": "Search:",
				"sUrl": "",
				"oPaginate": {
					"sFirst":    "First",
					"sPrevious": "Previous",
					"sNext":     "Next",
					"sLast":     "Last"
				}
			};
			
			
			/*
			 * Variable: aoData
			 * Purpose:  Store data information
			 * Scope:    jQuery.dataTable.classSettings 
			 * Notes:    This is an array of objects with the following parameters:
			 *   int: _iId - internal id for tracking
			 *   array: _aData - internal data - used for sorting / filtering etc
			 *   node: nTr - display node
			 */
			this.aoData = new Array();
			
			/*
			 * Variable: aiDisplay
			 * Purpose:  Array of indexes which are in the current display (after filtering etc)
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.aiDisplay = new Array();
			
			/*
			 * Variable: aiDisplayMaster
			 * Purpose:  Array of indexes for display - no filtering
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.aiDisplayMaster = new Array();
							
			/*
			 * Variable: aoColumns
			 * Purpose:  Store information about each column that is in use
			 * Scope:    jQuery.dataTable.classSettings 
			 */
			this.aoColumns = new Array();
			
			/*
			 * Variable: iNextId
			 * Purpose:  Store the next unique id to be used for a new row
			 * Scope:    jQuery.dataTable.classSettings 
			 */
			this.iNextId = 0;
			
			/*
			 * Variable: asDataSearch
			 * Purpose:  Search data array for regular expression searching
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.asDataSearch = new Array();
			
			/*
			 * Variable: sPreviousSearch
			 * Purpose:  Store the previous search incase we want to force a re-search
			 *   or compare the old search to a new one
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.sPreviousSearch = '';
			
			/*
			 * Variable: asPreSearchCols
			 * Purpose:  Store the previous search for each column
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.asPreSearchCols = new Array();
			
			/*
			 * Variable: nInfo
			 * Purpose:  Info display for user to see what records are displayed
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.nInfo = null;
			
			/*
			 * Variable: nProcessing
			 * Purpose:  Processing indicator div
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.nProcessing = null;
			
			/*
			 * Variable: iDisplayLength, iDisplayStart, iDisplayEnd
			 * Purpose:  Display length variables
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.iDisplayLength = 10;
			this.iDisplayStart = 0;
			this.iDisplayEnd = 10;
			
			/*
			 * Variable: aaSorting
			 * Purpose:  Sorting information
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.aaSorting = [ [0, 'asc'] ];
			
			/*
			 * Variable: asStripClasses
			 * Purpose:  Classes to use for the striping of a table
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.asStripClasses = new Array( 'odd', 'even' );
			
			/*
			 * Variable: fnRowCallback
			 * Purpose:  Call this function every time a row is inserted (draw)
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.fnRowCallback = null;
			
			/*
			 * Variable: fnHeaderCallback
			 * Purpose:  Callback function for the header on each draw
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.fnHeaderCallback = null;
			
			/*
			 * Variable: fnFooterCallback
			 * Purpose:  Callback function for the footer on each draw
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.fnFooterCallback = null;
			
			/*
			 * Variable: fnDrawCallback
			 * Purpose:  Callback function for the whole table on each draw
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.fnDrawCallback = null;
			
			/*
			 * Variable: fnInitComplete
			 * Purpose:  Callback function for when the table has been initalised
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.fnInitComplete = null;
			
			/*
			 * Variable: nFooter
			 * Purpose:  Footer node
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.nFooter = null;
			
			/*
			 * Variable: sTableId
			 * Purpose:  Cache the table ID for quick access
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.sTableId = "";
			
			/*
			 * Variable: nTable
			 * Purpose:  Cache the table node for quick access
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.nTable = null;
			
			/*
			 * Variable: iDefaultSortIndex
			 * Purpose:  Sorting index which will be used by default
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.iDefaultSortIndex = 0;
			
			/*
			 * Variable: bInitialised
			 * Purpose:  Indicate if all required information has been read in
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.bInitialised = false;
			
			/*
			 * Variable: nOpenRow
			 * Purpose:  Cache the open row node
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.nOpenRow = null;
			
			/*
			 * Variable: nPaginate, nPrevious, nNext
			 * Purpose:  Cache nodes for pagination
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.nPaginate = null;
			this.nPrevious = null;
			this.nNext = null;
			
			/*
			 * Variable: sDomPositioning
			 * Purpose:  Dictate the positioning that the created elements will take
			 * Scope:    jQuery.dataTable.classSettings
			 * Notes:    The following syntax is expected:
			 *   'l' - Length changing
			 *   'f' - Filtering input
			 *   't' - The table!
			 *   'i' - Information
			 *   'p' - Pagination
			 *   'r' - pRocessing
			 *   '<' and '>' - div elements
			 *   '<"class" and '>' - div with a class
			 *    Examples: '<"wrapper"flipt>', '<lf<t>ip>'
			 */
			this.sDomPositioning = 'lfrtip';
			
			/*
			 * Variable: sPaginationType
			 * Purpose:  Note which type of sorting should be used
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.sPaginationType = "two_button";
			
			/*
			 * Variable: sAjaxSource
			 * Purpose:  Source url for AJAX data for the table
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.sAjaxSource = null;
			
			/*
			 * Variable: iCookieDuration
			 * Purpose:  The cookie duration (for bStateSave) in seconds - default 2 hours
			 * Scope:    jQuery.dataTable.classSettings
			 */
			this.iCookieDuration = 60 * 60 * 2;
		}
		
		/*
		 * Variable: oApi
		 * Purpose:  Container for publicly exposed 'private' functions
		 * Scope:    jQuery.dataTable
		 */
		this.oApi = {};
		
		
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * API functions
		 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
		
		/*
		 * Function: fnDraw
		 * Purpose:  Redraw the table
		 * Returns:  -
		 * Inputs:   -
		 */
		this.fnDraw = function()
		{
			/* Find settings from table node */
			var oSettings = _fnSettingsFromNode( this[0] );
			
			_fnCalculateEnd( oSettings );
			_fnDraw( oSettings );
		}
		
		/*
		 * Function: fnFilter
		 * Purpose:  Filter the input based on data
		 * Returns:  -
		 * Inputs:   string:sInput - string to filter the table on
		 *           int:iColumn - optional - column to limit filtering to
		 *           bool:bEscapeRegex - optional - escape regex characters or not - default true
		 */
		this.fnFilter = function( sInput, iColumn, bEscapeRegex )
		{
			var oSettings = _fnSettingsFromNode( this[0] );
			
			if ( typeof iColumn == "undefined" || iColumn == null )
			{
				_fnFilterComplete( oSettings, sInput, 1, bEscapeRegex );
			}
			else
			{
				oSettings.asPreSearchCols[ iColumn ] = sInput;
				_fnFilterComplete( oSettings, oSettings.sPreviousSearch, 1, bEscapeRegex );
			}
		}
		
		/*
		 * Function: fnSettings
		 * Purpose:  Get the settings for a particular table for extern. manipulation
		 * Returns:  -
		 * Inputs:   -
		 */
		this.fnSettings = function( nNode  )
		{
			return _fnSettingsFromNode( this[0] );
		}
		
		/*
		 * Function: fnSort
		 * Purpose:  Sort the table by a particular row
		 * Returns:  -
		 * Inputs:   int:iCol - the data index to sort on. Note that this will
		 *   not match the 'display index' if you have hidden data entries
		 */
		this.fnSort = function( aaSort )
		{
			var oSettings = _fnSettingsFromNode( this[0] );
			oSettings.aaSorting = aaSort;
			_fnSort( oSettings );
		}
		
		/*
		 * Function: fnAddData
		 * Purpose:  Add new row(s) into the table
		 * Returns:  0 - ok
		 *           1 - length error
		 * Inputs:   array:mData - the data to be added. The length must match
		 *             the original data from the DOM
		 *           or
		 *           array array:mData - 2D array of data to be added
		 * Notes:    Warning - the refilter here will cause the table to redraw
		 *             starting at zero
		 * Notes:    Thanks to Yekimov Denis for contributing the basis for this function!
		 */
		this.fnAddData = function( mData )
		{
			/* Find settings from table node */
			var oSettings = _fnSettingsFromNode( this[0] );
			
			/* Check if we want to add multiple rows or not */
			if ( typeof mData[0] == "object" )
			{
				for ( var i=0 ; i<mData.length ; i++ )
				{
					if ( _fnAddData( oSettings, mData[i] ) == 1 )
					{
						return 1;
					}
				}
			}
			else
			{
				if ( _fnAddData( oSettings, mData ) == 1 )
				{
					return 1;
				}
			}
			
			oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
			
			/* Rebuild the search */
			_fnBuildSearchArray( oSettings, 1 );
			
			/* Re-sort */
			if ( oSettings.oFeatures.bSort )
			{
				_fnSort( oSettings );
				_fnSortingClasses( oSettings );
			}
			
			/* But we do need to re-filter or re-draw */
			if ( oSettings.oFeatures.bFilter )
			{
				_fnFilterComplete( oSettings, oSettings.sPreviousSearch );
			}
			else
			{
				_fnCalculateEnd( oSettings );
				_fnDraw( oSettings );
			}
			return 0;
		}
		
		/*
		 * Function: fnDeleteRow
		 * Purpose:  Remove a row for the table
		 * Returns:  array:aReturn - the row that was deleted
		 * Inputs:   int:iIndex - index of aoData to be deleted
		 * Notes:    This function requires a little explanation - we don't actually delete the data
		 *   from aoData - rather we remove it's references from aiDisplayMastr and aiDisplay. This
		 *   in effect prevnts DataTables from drawing it (hence deleting it) - it could be restored
		 *   if you really wanted. The reason for this is that actually removing the aoData object
		 *   would mess up all the subsequent indexes in the display arrays (they could be ajusted - 
		 *   but this appears to do what is required).
		 */
		this.fnDeleteRow = function( iAODataIndex, fnCallBack )
		{
			/* Find settings from table node */
			var oSettings = _fnSettingsFromNode( this[0] );
			
			/* Delete from the display master */
			for ( var i=0 ; i<oSettings.aiDisplayMaster.length ; i++ )
			{
				if ( oSettings.aiDisplayMaster[i] == iAODataIndex )
				{
					oSettings.aiDisplayMaster.splice( i, 1 );
					break;
				}
			}
			
			/* Delete from the current display index */
			for ( var i=0 ; i<oSettings.aiDisplay.length ; i++ )
			{
				if ( oSettings.aiDisplay[i] == iAODataIndex )
				{
					oSettings.aiDisplay.splice( i, 1 );
				}
			}
			
			/* Rebuild the search */
			_fnBuildSearchArray( oSettings, 1 );
			
			/* If there is a user callback function - call it */
			if ( typeof fnCallBack == "function" )
			{
				fnCallBack.call( this );
			}
			
			/* Check for an 'overflow' they case for dislaying the table */
			if ( oSettings.iDisplayStart > oSettings.aiDisplay.length )
			{
				oSettings.iDisplayStart -= oSettings.iDisplayLength;
			}
			
			_fnCalculateEnd( oSettings );
			_fnDraw( oSettings );
			
			return oSettings.aoData[iAODataIndex]._aData.slice();
		}
		
		/*
		 * Function: fnClearTable
		 * Purpose:  Quickly and simply clear a table
		 * Returns:  -
		 * Inputs:   -
		 * Notes:    Thanks to Yekimov Denis for contributing this function!
		 */
		this.fnClearTable = function()
		{
			/* Find settings from table node */
			var oSettings = _fnSettingsFromNode( this[0] );
			
			oSettings.aoData.length = 0;
			oSettings.aiDisplayMaster.length = 0;
			oSettings.aiDisplay.length = 0;
			_fnCalculateEnd( oSettings );
			_fnDraw( oSettings );
		}
		
		/*
		 * Function: fnOpen
		 * Purpose:  Open a display row (append a row after the row in question)
		 * Returns:  -
		 * Inputs:   node:nTr - the table row to 'open'
		 *           string:sHtml - the HTML to put into the row
		 *           string:sClass - class to give the new row
		 */
		this.fnOpen = function( nTr, sHtml, sClass )
		{
			/* Find settings from table node */
			var oSettings = _fnSettingsFromNode( this[0] );
			
			/* Remove an old open row if there is one */
			if ( oSettings.nOpenRow != null )
			{
				this.fnClose();
			}
			
			var nNewRow = document.createElement("tr");
			var nNewCell = document.createElement("td");
			nNewRow.appendChild( nNewCell );
			nNewRow.className = sClass;
			nNewCell.colSpan = oSettings.aoColumns.length; // XXX - does this need to be visble columns?
			nNewCell.innerHTML = sHtml;
			
			$(nNewRow).insertAfter(nTr);
			oSettings.nOpenRow = nNewRow;
		}
		
		/*
		 * Function: fnClose
		 * Purpose:  Close a display row
		 * Returns:  -
		 * Inputs:   -
		 */
		this.fnClose = function()
		{
			/* Find settings from table node */
			var oSettings = _fnSettingsFromNode( this[0] );
			
			$(oSettings.nOpenRow).remove();
			oSettings.nOpenRow = null;
		}
		
		/*
		 * Function: fnDecrement
		 * Purpose:  Decremenet all numbers bigger than iMatch (for deleting)
		 * Returns:  -
		 * Inputs:   int:iMatch - decrement numbers bigger than this
		 *           int:iIndex - index of the data to decrement
		 * Notes:    Depreciated - don't use... :-)
		 */
		this.fnDecrement = function( iMatch, iIndex )
		{
			if ( typeof iIndex == 'undefined' )
				iIndex = 0;
			
			/* Find settings from table node */
			var oSettings = _fnSettingsFromNode( this[0] );
			
			for ( var i=0 ; i<oSettings.aaDataMaster.length ; i++ )
			{
				if ( oSettings.aaDataMaster[i][iIndex]*1 > iMatch )
				{
					oSettings.aaDataMaster[i][iIndex] = (oSettings.aaDataMaster[i][iIndex]*1) - 1;
				}
			}
		}
		
		/*
		 * Function: fnGetData
		 * Purpose:  Return an array with the data which is used to make up the table
		 * Returns:  array array string: 2d data array ([row][column]) or array string: 1d data array
		 * Inputs:   int:iRow - optional - if present then the array returned will be the data for
		 *             the row with the index 'iRow'
		 */
		this.fnGetData = function( iRow )
		{
			var oSettings = _fnSettingsFromNode( this[0] );
			var aaData = _fnGetDataMaster( oSettings );
			
			if ( typeof iRow != 'undefined' )
			{
				return aaData[iRow];
			}
			return aaData;
		}
		
		/*
		 * Function: fnGetNodes
		 * Purpose:  Return an array with the TR nodes used for drawing the table
		 * Returns:  array node: TR elements
		 * Inputs:   -
		 */
		this.fnGetNodes = function()
		{
			return _fnGetTrNodes( _fnSettingsFromNode( this[0] ) );
		}
		
		/*
		 * Function: fnGetPosition
		 * Purpose:  Get the array indexes of a particular cell from it's DOM element
		 * Returns:  int: - row index, or array[ int, int ]: - row index and column index
		 * Inputs:   node:nNode - this can either be a TR or a TD in the table, the return is
		 *             dependent on this input
		 */
		this.fnGetPosition = function( nNode )
		{
			var oSettings = _fnSettingsFromNode( this[0] );
			
			if ( nNode.nodeName == "TR" )
			{
				for ( var i=0 ; i<oSettings.aoData.length ; i++ )
				{
					if ( oSettings.aoData[i].nTr == nNode )
					{
						return i;
					}
				}
			}
			else if ( nNode.nodeName == "TD" )
			{
				for ( var i=0 ; i<oSettings.aoData.length ; i++ )
				{
					var iCorrector = 0;
					for ( var j=0 ; j<oSettings.aoColumns.length ; j++ )
					{
						if ( oSettings.aoColumns[j].bVisible )
						{
							if ( oSettings.aoData[i].nTr.getElementsByTagName('td')[j-iCorrector] == nNode )
							{
								return [ i, j-iCorrector ];
							}
						}
						else
						{
							iCorrector++;
						}
					}
				}
			}
			return null;
		}
		
		/*
		 * Plugin API functions
		 * 
		 * This call will add the functions which are defined in $.fn.dataTableExt.oApi to the
		 * DataTables object, providing a rather nice way to allow plug-in API functions. Note that
		 * this is done here, so that API function can actually override the built in API functions if
		 * required for a particular purpose. Also the local bApi flag is used for allowing DataTables
		 * to decide (later on) if it should expose it's private functions or not.
		 */
		var bApi = false;
		for ( var sFunc in $.fn.dataTableExt.oApi )
		{
			/*
			 * Function: anon
			 * Purpose:  Wrap the plug-in API functions in order to provide the settings as 1st arg and
			 *   execute in this scope
			 * Returns:  -
			 * Inputs:   -
			 */
			this[sFunc] = function() {
				return $.fn.dataTableExt.oApi[sFunc].call( this, _fnSettingsFromNode( this[0] ) );
			}
			bApi = true;
		}
		
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * Local functions
		 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * Initalisation
		 */
		
		/*
		 * Function: _fnInitalise
		 * Purpose:  Draw the table for the first time, adding all required features
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnInitalise ( oSettings )
		{
			/* Ensure that the table data is fully initialised */
			if ( oSettings.bInitialised == false )
			{
				setTimeout( function(){ _fnInitalise( oSettings ) }, 200 );
				return;
			}
			
			/* Show the display HTML options */
			_fnAddOptionsHtml( oSettings );
			
			/* Draw the headers for the table */
			_fnDrawHead( oSettings );
			
			/* If there is default sorting required - let's do it. The sort function
			 * will do the drawing for us. Otherwise we draw the table
			 */
			if ( oSettings.oFeatures.bSort )
			{
				_fnSort( oSettings );
			}
			else
			{
				oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
				_fnCalculateEnd( oSettings );
				_fnDraw( oSettings );
			}
			
			/* if there is an ajax source - ? */
			if ( oSettings.sAjaxSource != null )
			{
				_fnProcessingDisplay( oSettings, true );
				
				$.getJSON( oSettings.sAjaxSource, null, function(json) {
					/* Got the data - add it to the table */
					for ( var i=0 ; i<json.aaData.length ; i++ )
					{
						_fnAddData( oSettings, json.aaData[i] );
					}
					
					oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
					_fnCalculateEnd( oSettings );
					_fnDraw( oSettings );
					if ( oSettings.oFeatures.bSort )
					{
						_fnSortingClasses( oSettings );
					}
					_fnProcessingDisplay( oSettings, false );
				} );
			}
			
			/*
			 * Add the sorting classes to the header and the body (if needed).
			 * Reason for doing it here after the first draw is to stop classes being applied to the
			 * 'static' table.
			 */
			if ( oSettings.oFeatures.bSort )
			{
				_fnSortingClasses( oSettings );
			}
			
			/* Run the init callback if there is one */
			if ( typeof oSettings.fnInitComplete == 'function' )
			{
				oSettings.fnInitComplete();
			}
		}
		
		/*
		 * Function: _fnLanguageProcess
		 * Purpose:  Copy language variables from remote object to a local one
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 *           object:oLanguage - Language information
		 */
		function _fnLanguageProcess( oSettings, oLanguage )
		{
			if ( typeof oLanguage.sProcessing != 'undefined' )
				oSettings.oLanguage.sProcessing = oLanguage.sProcessing;
			
			if ( typeof oLanguage.sLengthMenu != 'undefined' )
				oSettings.oLanguage.sLengthMenu = oLanguage.sLengthMenu;
			
			if ( typeof oLanguage.sZeroRecords != 'undefined' )
				oSettings.oLanguage.sZeroRecords = oLanguage.sZeroRecords;
			
			if ( typeof oLanguage.sInfo != 'undefined' )
				oSettings.oLanguage.sInfo = oLanguage.sInfo;
			
			if ( typeof oLanguage.sInfoEmpty != 'undefined' )
				oSettings.oLanguage.sInfoEmpty = oLanguage.sInfoEmpty;
			
			if ( typeof oLanguage.sInfoFiltered != 'undefined' )
				oSettings.oLanguage.sInfoFiltered = oLanguage.sInfoFiltered;
			
			if ( typeof oLanguage.sInfoPostFix != 'undefined' )
				oSettings.oLanguage.sInfoPostFix = oLanguage.sInfoPostFix;
			
			if ( typeof oLanguage.sSearch != 'undefined' )
				oSettings.oLanguage.sSearch = oLanguage.sSearch;
			
			if ( typeof oLanguage.oPaginate != 'undefined' )
			{
				if ( typeof oLanguage.oPaginate != 'undefined' )
					oSettings.oLanguage.oPaginate.sFirst = oLanguage.oPaginate.sFirst;
				
				if ( typeof oLanguage.oPaginate != 'undefined' )
					oSettings.oLanguage.oPaginate.sPrevious = oLanguage.oPaginate.sPrevious;
				
				if ( typeof oLanguage.oPaginate != 'undefined' )
					oSettings.oLanguage.oPaginate.sNext = oLanguage.oPaginate.sNext;
				
				if ( typeof oLanguage.oPaginate != 'undefined' )
					oSettings.oLanguage.oPaginate.sLast = oLanguage.oPaginate.sLast;
			}
			
			_fnInitalise( oSettings );
		}
		
		/*
		 * Function: _fnAddColumn
		 * Purpose:  Add a column to the list used for the table
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 *           oOptions - object with sType, bVisible and bSearchable
		 * Notes:    All options in enter column can be over-ridden by the user
		 *   initialisation of dataTables
		 */
		function _fnAddColumn( oSettings, oOptions )
		{
			oSettings.aoColumns[ oSettings.aoColumns.length++ ] = {
				"sType": null,
				"_bAutoType": true,
				"bVisible": true,
				"bSearchable": true,
				"bSortable": true,
				"sTitle": null,
				"sWidth": null,
				"sClass": null,
				"fnRender": null,
				"iDataSort": oSettings.aoColumns.length-1
			};
			
			/* User specified column options */
			if ( typeof oOptions != 'undefined' && oOptions != null )
			{
				var iLength = oSettings.aoColumns.length-1;
				
				if ( typeof oOptions.sType != 'undefined' )
				{
					oSettings.aoColumns[ iLength ].sType = oOptions.sType;
					oSettings.aoColumns[ iLength ]._bAutoType = false;
				}
				
				if ( typeof oOptions.bVisible != 'undefined' )
					oSettings.aoColumns[ iLength ].bVisible = oOptions.bVisible;
				
				if ( typeof oOptions.bSearchable != 'undefined' )
					oSettings.aoColumns[ iLength ].bSearchable = oOptions.bSearchable;
				
				if ( typeof oOptions.bSortable != 'undefined' )
					oSettings.aoColumns[ iLength ].bSortable = oOptions.bSortable;
				
				if ( typeof oOptions.sTitle != 'undefined' )
					oSettings.aoColumns[ iLength ].sTitle = oOptions.sTitle;
				
				if ( typeof oOptions.sWidth != 'undefined' )
					oSettings.aoColumns[ iLength ].sWidth = oOptions.sWidth;
				
				if ( typeof oOptions.sClass != 'undefined' )
					oSettings.aoColumns[ iLength ].sClass = oOptions.sClass;
				
				if ( typeof oOptions.fnRender != 'undefined' )
					oSettings.aoColumns[ iLength ].fnRender = oOptions.fnRender;
				
				if ( typeof oOptions.iDataSort != 'undefined' )
					oSettings.aoColumns[ iLength ].iDataSort = oOptions.iDataSort;
			}
			
			/* Add a column specific filter */
			oSettings.asPreSearchCols[ oSettings.asPreSearchCols.length++ ] = "";
		}
		
		
		/*
		 * Function: _fnAddData
		 * Purpose:  Add a data array to the table, creating DOM node etc
		 * Returns:  int: - 0 successful, 1 failed
		 * Inputs:   object:oSettings - dataTables settings object
		 *           array:aData - data array to be added
		 */
		function _fnAddData ( oSettings, aData )
		{
			/* Sanity check the length of the new array */
			if ( aData.length != oSettings.aoColumns.length )
			{
				return 1;
			}
			
			/* Create the object for storing information about this new row */
			var iThisIndex = oSettings.aoData.length;
			oSettings.aoData.push( {
				"_iId": oSettings.iNextId++,
				"_aData": aData.slice(),
				"nTr": document.createElement('tr')
			} );
			
			/* Create the cells */
			var nTd;
			for ( var i=0 ; i<aData.length ; i++ )
			{
				if ( oSettings.aoColumns[i].bVisible )
				{
					nTd = document.createElement('td');
					
					nTd.innerHTML = ( typeof oSettings.aoColumns[i].fnRender == 'function' ) ?
						oSettings.aoColumns[i].fnRender( {
								"iDataRow": iThisIndex,
								"iDataColumn": i,
								"aData": _fnGetDataMaster( oSettings )
							} ) :
						nTd.innerHTML = aData[i];
					
					if ( oSettings.aoColumns[i].sClass != null )
					{
						nTd.className = oSettings.aoColumns[i].sClass;
					}
					
					/* See if we should auto-detect the column type */
					if ( oSettings.aoColumns[i]._bAutoType && oSettings.aoColumns[i].sType != 'string' )
					{
						/* Attempt to auto detect the type - same as _fnGatherData() */
						if ( oSettings.aoColumns[i].sType == null )
						{
							oSettings.aoColumns[i].sType = _fnDetectType( aData[i] );
						}
						else if ( oSettings.aoColumns[i].sType == "date" || 
						          oSettings.aoColumns[i].sType == "numeric" )
						{
							oSettings.aoColumns[i].sType = _fnDetectType( aData[i] );
						}
					}
					
					oSettings.aoData[iThisIndex].nTr.appendChild( nTd );
				}
			}
			
			/* Add to the display array */
			oSettings.aiDisplayMaster.push( iThisIndex );
			
			return 0;
		}
		
		
		/*
		 * Function: _fnGatherData
		 * Purpose:  Read in the data from the target table
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnGatherData( oSettings )
		{
			var nDataNodes;
			var iDataLength = $('tbody tr').length;
			
			/* Sanity check the number of columns and cells */
			if ( $('thead th', oSettings.nTable).length != oSettings.aoColumns.length )
			{
				alert( 'Warning - columns do not match' );
			}
			
			/* Get the header and footer nodes */
			oSettings.nHeader = $('thead', oSettings.nTable)[0];
			if ( typeof $('tfoot', oSettings.nTable)[0] != 'undefined' )
			{
				oSettings.nFooter = $('tfoot', oSettings.nTable)[0];
			}
			
			/*
			 * Process by row first
			 * Add the data object for the whole table - storing the tr node
			 */
			$('tbody tr', oSettings.nTable).each( function() {
				var iThisIndex = oSettings.aoData.length;
				oSettings.aoData.push( {
					"_iId": oSettings.iNextId++,
					"_aData": new Array(),
					"nTr": this
				} );
				
				oSettings.aiDisplayMaster.push( iThisIndex );
				
				var aLocalData = oSettings.aoData[iThisIndex]._aData;
				$('td', this).each( function( i ) {
					aLocalData[i] = this.innerHTML;
				} );
			} );
			
			/*
			 * Now process by column
			 */
			var iCorrector = 0;
			for ( var i=0 ; i<oSettings.aoColumns.length ; i++ )
			{
				/* Get the title of the column - unless there is a user set one */
				if ( oSettings.aoColumns[i].sTitle == null )
				{
					oSettings.aoColumns[i].sTitle = $('thead th:nth-child('+(i+1)+')', oSettings.nTable).html();
				}
				
				/* See if we should auto detect column type */
				if ( oSettings.aoColumns[i]._bAutoType )
				{
					var iLoop = oSettings.aoData.length;
					
					if ( iLoop == 0 )
					{
						oSettings.aoColumns[i].sType = 'string';
					}
					
					/* Attempt to auto detect the type */
					for ( var j=0 ; j<iLoop ; j++ )
					{
						if ( oSettings.aoColumns[i].sType == null )
						{
							oSettings.aoColumns[i].sType = _fnDetectType( oSettings.aoData[j]._aData[i] );
						}
						else if ( oSettings.aoColumns[i].sType == "date" || 
						          oSettings.aoColumns[i].sType == "numeric" )
						{
							/* If type is date or numeric - ensure that all collected data
							 * in the column is of the same type
							 */
							oSettings.aoColumns[i].sType = _fnDetectType( oSettings.aoData[j]._aData[i] );
						}
						/* The else would be 'type = string' we don't want to do anything
						 * if that is the case
						 */
					}
				}
				
				/* Use cell rendering if needed */
				if ( typeof oSettings.aoColumns[i].fnRender == 'function' )
				{
					var iLoop = oSettings.aoData.length;
					var aData = _fnGetDataMaster( oSettings );
					for ( var j=0 ; j<iLoop ; j++ )
					{
						oSettings.aoData[j].nTr.getElementsByTagName('td')[ i-iCorrector ].innerHTML = 
							oSettings.aoColumns[i].fnRender( {
								"iDataRow": j,
								"iDataColumn": i,
								"aData": aData
							} );
					}
				}
				
				/* Remove hidden columns from the DOM */
				if ( !oSettings.aoColumns[i].bVisible )
				{
					var iLoop = oSettings.aoData.length;
					for ( var j=0 ; j<iLoop ; j++ )
					{
						var nRemove = oSettings.aoData[j].nTr.getElementsByTagName('td')[ i-iCorrector ];
						nRemove.parentNode.removeChild( nRemove );
					}
					iCorrector++;
				}
			}	
		}
		
		
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * Drawing functions
		 */
		
		/*
		 * Function: _fnDrawHead
		 * Purpose:  Create the HTML header for the table
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnDrawHead( oSettings )
		{
			var nThOriginals = $('thead th', oSettings.nTable);
			
			/* If there is a header in place and it's the right size - then use it */
			if ( nThOriginals.length == oSettings.aoColumns.length )
			{
				/* We've got a thead from the DOM, so remove hidden columns and apply width to vis cols */
				for ( var i=0 ; i<oSettings.aoColumns.length ; i++ )
				{
					if ( oSettings.aoColumns[i].bVisible )
					{
						/* Set width */
						if ( oSettings.aoColumns[i].sWidth != null )
						{
							nThOriginals[i].style.width = oSettings.aoColumns[i].sWidth;
						}
						
						/* Set the title of the column if it is user defined (not what was auto detected) */
						if ( oSettings.aoColumns[i].sTitle != nThOriginals[i].innerHTML )
						{
							nThOriginals[i].innerHTML = oSettings.aoColumns[i].sTitle;
						}
					}
					else
					{
						nThOriginals[i].parentNode.removeChild( nThOriginals[i] );
					}
				}
			}
			else
			{
				/* We don't have a header in the DOM - so we are going to have to create one */
				var nTh;
				var nTr = document.createElement( "tr" );
				
				for ( var i=0 ; i<oSettings.aoColumns.length ; i++ )
				{
					if ( oSettings.aoColumns[i].bVisible )
					{
						nTh = document.createElement( "th" );
						
						if ( typeof nThOriginals[i] != "undefined" && nThOriginals[i].className != "" )
						{
							nTh.className = nThOriginals[i].className;
						}
						
						if ( oSettings.aoColumns[i].sWidth != null )
						{
							nTh.style.width = oSettings.aoColumns[i].sWidth;
						}
						
						nTh.innerHTML = oSettings.aoColumns[i].sTitle;
						nTr.appendChild( nTh );
					}
				}
				$('thead', oSettings.nTable).html( '' )[0].appendChild( nTr );
			}
			
			
			/* Add sort listener */
			if ( oSettings.oFeatures.bSort )
			{
				$('thead th', oSettings.nTable).click( function( e ) {
					/* Convert the column index to data index */
					var iDataIndex = $("thead th", oSettings.nTable).index(this);
					
					/* Take account of hidden columns */
					iDataIndex = _fnVisibleToColumnIndex( oSettings, iDataIndex );
					
					/* If the column is not sortable - don't to anything */
					if ( oSettings.aoColumns[iDataIndex].bSortable == false )
					{
						return;
					}
					
					_fnProcessingDisplay( oSettings, true );
					
					/*
					 * XXX This is nasty has hell - but I can't see another way around it. If anyone does
					 * know of a good way, please let me know so I can include it in later versions.
					 * Basically the issue here is that the Javascript engine in modern browsers doesn't 
					 * appear to allow the rendering engine to update the display while it is still excuting
					 * it's thread (well - it does but only after long intervals). This means that the 
					 * 'processing' display doesn't appear for a table sort. To break the js thread up a bit
					 * I force an execution break by using setTimeout (see - nasty as hell...).
					 */
					setTimeout( function() {
						if ( e.shiftKey )
						{
							/* If the shift key is pressed then we are multipe column sorting */
							var bFound = false;
							for ( var i=0 ; i<oSettings.aaSorting.length ; i++ )
							{
								if ( oSettings.aaSorting[i][0] == iDataIndex )
								{
									if ( oSettings.aaSorting[i][1] == "asc" )
									{
										oSettings.aaSorting[i][1] = "desc";
									}
									else
									{
										oSettings.aaSorting.splice( i, 1 );
									}
									bFound = true;
									break;
								}
							}
							
							if ( bFound == false )
							{
								oSettings.aaSorting.push( new Array( iDataIndex, "asc" ) );
							}
						}
						else
						{
							/* If no shift key then single column sort */
							if ( oSettings.aaSorting.length == 1 && oSettings.aaSorting[0][0] == iDataIndex )
							{
								oSettings.aaSorting[0][1] = oSettings.aaSorting[0][1]=="asc" ? "desc" : "asc";
							}
							else
							{
								oSettings.aaSorting.splice( 0, oSettings.aaSorting.length );
								oSettings.aaSorting.push( new Array( iDataIndex, "asc" ) );
							}
						}
						
						/* Remove previous sort */
						_fnSortingClasses( oSettings );
						
						/* Run the sort */
						_fnSort( oSettings );
						
						_fnProcessingDisplay( oSettings, false );
					}, 0 ); /* end nasty-ness */
				} ); /* end click */
				
				/* Take the brutal approach to cancelling text selection due to the shift key */
				$('thead th', oSettings.nTable).mousedown( function () {
					this.onselectstart = function() {return false};
					return false;
				} );
			} /* if feature sort */
			
			/* Set an absolute width for the table such that pagination doesn't
			 * cause the table to resize
			 */
			if ( oSettings.oFeatures.bAutoWidth )
			{
				oSettings.nTable.style.width = oSettings.nTable.offsetWidth+"px";
			}
		}
		
		
		/*
		 * Function: _fnDraw
		 * Purpose:  Insert the required TR nodes into the table for display
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnDraw( oSettings )
		{
			var anRows = new Array();
			var iRowCount = 0;
			var iStrips = oSettings.asStripClasses.length;
			
			if ( oSettings.aiDisplay.length != 0 )
			{
				for ( var j=oSettings.iDisplayStart ; j<oSettings.iDisplayEnd ; j++ )
				{
					var nRow = oSettings.aoData[ oSettings.aiDisplay[j] ].nTr;
					
					/* Remove any old stripping classes and then add the new one */
					$(nRow).removeClass( oSettings.asStripClasses.join(' ') );
					$(nRow).addClass( oSettings.asStripClasses[ iRowCount % iStrips ] );
					
					/* Custom row callback function - might want to manipule the row */
					if ( typeof oSettings.fnRowCallback == "function" )
					{
						anRows[ iRowCount ] = oSettings.fnRowCallback( nRow, 
							oSettings.aoData[ oSettings.aiDisplay[j] ]._aData, iRowCount, j );
					}
					
					anRows.push( nRow );
					iRowCount++;
				}
			}
			else
			{
				/* Table is empty - create a row with an empty message in it */
				anRows[ 0 ] = document.createElement( 'tr' );
				var nTd = document.createElement( 'td' );
				nTd.setAttribute( 'valign', "top" );
				nTd.colSpan = oSettings.aoColumns.length;
				nTd.style.textAlign = "center";
				nTd.innerHTML = oSettings.oLanguage.sZeroRecords;
				
				anRows[ iRowCount ].appendChild( nTd );
			}
			
			/* Callback the header and footer custom funcation if there is one */
			if ( typeof oSettings.fnHeaderCallback == 'function' )
			{
				oSettings.fnHeaderCallback( $('thead tr', oSettings.nTable)[0], 
					_fnGetDataMaster( oSettings ), oSettings.iDisplayStart, oSettings.iDisplayEnd,
					oSettings.aiDisplay );
			}
			
			if ( typeof oSettings.fnFooterCallback == 'function' )
			{
				oSettings.fnFooterCallback( oSettings.nFooter, 
					_fnGetDataMaster( oSettings ), oSettings.iDisplayStart, oSettings.iDisplayEnd,
					oSettings.aiDisplay );
			}
			
			/* 
			 * Need to remove any old row from the display - note we can't just empty the tbody using
			 * .html('') since this will unbind the jQuery event handlers (even although the node still
			 * exists!)
			 */
			var nTrs = $('tbody tr', oSettings.nTable);
			for ( var i=0 ; i<nTrs.length ; i++ )
			{
				nTrs[i].parentNode.removeChild( nTrs[i] );
			}
			
			/* Put the draw table into the dom */
			var nBody = $('tbody', oSettings.nTable);
			for ( var i=0 ; i<anRows.length ; i++ )
			{
				nBody[0].appendChild( anRows[i] );
			}
			
			/* Update the pagination display buttons */
			if ( oSettings.oFeatures.bPaginate )
			{
				$.fn.dataTableExt.oPagination[ oSettings.sPaginationType ].fnUpdate( oSettings, function( oSettings ) {
					_fnCalculateEnd( oSettings );
					_fnDraw( oSettings );
				} );
			}
			
			/* Show information about the table */
			if ( oSettings.oFeatures.bInfo )
			{
				/* Update the information */
				if ( oSettings.aiDisplay.length == 0 && 
					   oSettings.aiDisplay.length == oSettings.aiDisplayMaster.length )
				{
					oSettings.nInfo.innerHTML = 
						oSettings.oLanguage.sInfoEmpty +' '+ oSettings.oLanguage.sInfoPostFix;
				}
				else if ( oSettings.aiDisplay.length == 0 )
				{
					oSettings.nInfo.innerHTML = oSettings.oLanguage.sInfoEmpty +' '+ 
						oSettings.oLanguage.sInfoFiltered.replace('_MAX_', 
							oSettings.aiDisplayMaster.length) +' '+ oSettings.oLanguage.sInfoPostFix;
				}
				else if ( oSettings.aiDisplay.length == oSettings.aiDisplayMaster.length )
				{
					oSettings.nInfo.innerHTML = 
						oSettings.oLanguage.sInfo.
							replace('_START_',oSettings.iDisplayStart+1).
							replace('_END_',oSettings.iDisplayEnd).
							replace('_TOTAL_',oSettings.aiDisplay.length) +' '+ 
						oSettings.oLanguage.sInfoPostFix;
				}
				else
				{
					oSettings.nInfo.innerHTML = 
						oSettings.oLanguage.sInfo.
							replace('_START_',oSettings.iDisplayStart+1).
							replace('_END_',oSettings.iDisplayEnd).
							replace('_TOTAL_',oSettings.aiDisplay.length) +' '+ 
						oSettings.oLanguage.sInfoFiltered.replace('_MAX_', oSettings.aiDisplayMaster.length) +' '+ 
						oSettings.oLanguage.sInfoPostFix;
				}
			}
			
			/* Save the table state on each draw */
			_fnSaveState( oSettings );
			
			/* Drawing is finished - call the callback if there is one */
			if ( typeof oSettings.fnDrawCallback == 'function' )
			{
				oSettings.fnDrawCallback();
			}
		}
		
		
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * Options (features) HTML
		 */
		
		/*
		 * Function: _fnAddOptionsHtml
		 * Purpose:  Add the options to the page HTML for the table
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnAddOptionsHtml ( oSettings )
		{
			/*
			 * Create a temporary, empty, div which we can later on replace with what we have generated
			 * we do it this way to rendering the 'options' html offline - speed :-)
			 */
			var nHolding = document.createElement( 'div' );
			oSettings.nTable.parentNode.insertBefore( nHolding, oSettings.nTable );
			
			/* 
			 * All DataTables are wrapped in a div - this is not currently optional - backwards 
			 * compatability. It can be removed if you don't want it.
			 */
			var nWrapper = document.createElement( 'div' );
			nWrapper.className = "dataTables_wrapper";
			if ( oSettings.sTableId != "" )
			{
				nWrapper.setAttribute( 'id', oSettings.sTableId+'_wrapper' );
			}
			
			/* Track where we want to insert the option */
			var nInsertNode = nWrapper;
			
			/* IE don't treat strings as arrays */
			var sDom = oSettings.sDomPositioning.split('');
			
			/* Loop over the user set positioning and place the elements as needed */
			for ( var i=0 ; i<sDom.length ; i++ )
			{
				var cOption = sDom[i];
				
				if ( cOption == '<' )
				{
					/* New container div */
					var nNewNode = document.createElement( 'div' );
					
					/* Check to see if we should append a class name to the container */
					var cNext = sDom[i+1];
					if ( cNext == "'" || cNext == '"' )
					{
						var sClass = "";
						var j = 2;
						while ( sDom[i+j] != cNext )
						{
							sClass += sDom[i+j];
							j++;
						}
						nNewNode.className = sClass;
						i += j; /* Move along the position array */
					}
					
					nInsertNode.appendChild( nNewNode );
					nInsertNode = nNewNode;
				}
				else if ( cOption == '>' )
				{
					/* End container div */
					nInsertNode = nInsertNode.parentNode;
				}
				else if ( cOption == 'l' && oSettings.oFeatures.bPaginate && oSettings.oFeatures.bLengthChange )
				{
					/* Length */
					nInsertNode.appendChild( _fnFeatureHtmlLength( oSettings ) );
				}
				else if ( cOption == 'f' && oSettings.oFeatures.bFilter )
				{
					/* Filter */
					nInsertNode.appendChild( _fnFeatureHtmlFilter( oSettings ) );
				}
				else if ( cOption == 'r' && oSettings.oFeatures.bProcessing )
				{
					/* pRocessing */
					nInsertNode.appendChild( _fnFeatureHtmlProcessing( oSettings ) );
				}
				else if ( cOption == 't' )
				{
					/* Table */
					nInsertNode.appendChild( oSettings.nTable );
				}
				else if ( cOption ==  'i' && oSettings.oFeatures.bInfo )
				{
					/* Info */
					nInsertNode.appendChild( _fnFeatureHtmlInfo( oSettings ) );
				}
				else if ( cOption == 'p' && oSettings.oFeatures.bPaginate )
				{
					/* Pagination */
					nInsertNode.appendChild( _fnFeatureHtmlPaginate( oSettings ) );
				}
			}
			
			/* Built our DOM structure - replace the holding div with what we want */
			nHolding.parentNode.replaceChild( nWrapper, nHolding );
		}
		
		
		/*
		 * Function: _fnFeatureHtmlFilter
		 * Purpose:  Generate the node required for filtering text
		 * Returns:  node
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnFeatureHtmlFilter ( oSettings )
		{
			var nFilter = document.createElement( 'div' );
			if ( oSettings.sTableId != "" )
			{
				nFilter.setAttribute( 'id', oSettings.sTableId+'_filter' );
			}
			nFilter.className = "dataTables_filter";
			nFilter.innerHTML = oSettings.oLanguage.sSearch+' <input type="text" value="'+
				oSettings.sPreviousSearch.replace('"','&quot;')+'" />';
			
			$("input", nFilter).keyup( function(e) {
				_fnFilterComplete( oSettings, this.value );
			} );
			
			return nFilter;
		}
		
		
		/*
		 * Function: _fnFeatureHtmlInfo
		 * Purpose:  Generate the node required for the info display
		 * Returns:  node
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnFeatureHtmlInfo ( oSettings )
		{
			var nInfo = document.createElement( 'div' );
			oSettings.nInfo = nInfo;
			
			if ( oSettings.sTableId != "" )
			{
				oSettings.nInfo.setAttribute( 'id', oSettings.sTableId+'_info' );
			}
			oSettings.nInfo.className = "dataTables_info";
			return nInfo;
		}
		
		
		/*
		 * Function: _fnFeatureHtmlPaginate
		 * Purpose:  Generate the node required for default pagination
		 * Returns:  node
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnFeatureHtmlPaginate ( oSettings )
		{
			var nPaginate = document.createElement( 'div' );
			nPaginate.className = "dataTables_paginate";
			
			oSettings.nPaginate = nPaginate;
			$.fn.dataTableExt.oPagination[ oSettings.sPaginationType ].fnInit( oSettings, function( oSettings ) {
				_fnCalculateEnd( oSettings );
				_fnDraw( oSettings );
			} );
			return nPaginate;
		}
		
		
		/*
		 * Function: _fnFeatureHtmlLength
		 * Purpose:  Generate the node required for user display length changing
		 * Returns:  node
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnFeatureHtmlLength ( oSettings )
		{
			/* This can be overruled by not using the _MENU_ var/macro in the language variable */
			var sName = (oSettings.sTableId == "") ? "" : 'name="'+oSettings.sTableId+'_length"';
			var sStdMenu = 
				'<select size="1" '+sName+'>'+
					'<option value="10">10</option>'+
					'<option value="25">25</option>'+
					'<option value="50">50</option>'+
					'<option value="100">100</option>'+
				'</select>';
			
			var nLength = document.createElement( 'div' );
			if ( oSettings.sTableId != "" )
			{
				nLength.setAttribute( 'id', oSettings.sTableId+'_length' );
			}
			nLength.className = "dataTables_length";
			nLength.innerHTML = oSettings.oLanguage.sLengthMenu.replace( '_MENU_', sStdMenu );
			
			/*
			 * Set the length to the current display length - thanks to Andrea Pavlovic for this fix,
			 * and Stefan Skopnik for fixing the fix!
			 */
			$('select option[value="'+oSettings.iDisplayLength+'"]',nLength).attr("selected",true);
			
			$('select', nLength).change( function() {
				oSettings.iDisplayLength = parseInt($(this).val());
				
				_fnCalculateEnd( oSettings );
				
				/* If we have space to show extra rows (backing up from the end point - then do so */
				if ( oSettings.iDisplayEnd == oSettings.aiDisplay.length )
				{
					oSettings.iDisplayStart = oSettings.iDisplayEnd - oSettings.iDisplayLength;
					if ( oSettings.iDisplayStart < 0 )
					{
						oSettings.iDisplayStart = 0;
					}
				}
				
				_fnDraw( oSettings );
			} );
			
			return nLength;
		}
		
		
		/*
		 * Function: _fnFeatureHtmlProcessing
		 * Purpose:  Generate the node required for the processing node
		 * Returns:  node
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnFeatureHtmlProcessing ( oSettings )
		{
			var nProcessing = document.createElement( 'div' );
			oSettings.nProcessing = nProcessing;
			
			if ( oSettings.sTableId != "" )
			{
				oSettings.nProcessing.setAttribute( 'id', oSettings.sTableId+'_processing' );
			}
			oSettings.nProcessing.appendChild( document.createTextNode( oSettings.oLanguage.sProcessing ) );
			oSettings.nProcessing.className = "dataTables_processing";
			oSettings.nProcessing.style.visibility = "hidden";
			oSettings.nTable.parentNode.insertBefore( oSettings.nProcessing, oSettings.nTable );
			
			return nProcessing;
		}
		
		
		/*
		 * Function: _fnProcessingDisplay
		 * Purpose:  Display or hide the processing indicator
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 *           bool:
		 *   true - show the processing indicator
		 *   false - don't show
		 */
		function _fnProcessingDisplay ( oSettings, bShow )
		{
			if ( oSettings.oFeatures.bProcessing )
			{
				if ( bShow )
					oSettings.nProcessing.style.visibility = "visible";
				else
					oSettings.nProcessing.style.visibility = "hidden";
			}
		}
		
		
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * Filtering
		 */
		
		/*
		 * Function: _fnFilterComplete
		 * Purpose:  Filter the table using both the global filter and column based filtering
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 *           string:sInput - string to filter on
		 *           int:iForce - optional - force a research of the master array (1) or not (undefined or 0)
		 *           bool:bEscapeRegex - optional - default true
		 */
		function _fnFilterComplete ( oSettings, sInput, iForce, bEscapeRegex )
		{
			if ( typeof bEscapeRegex == 'undefined' )
			{
				bEscapeRegex = true;
			}
			
			/* Filter on everything */
			_fnFilter( oSettings, sInput, iForce, bEscapeRegex );
			
			/* Now do the individual column filter */
			for ( var i=0 ; i<oSettings.asPreSearchCols.length ; i++ )
			{
				_fnFilterColumn( oSettings, oSettings.asPreSearchCols[i], i, bEscapeRegex );
			}
			
			/* Redraw the table */
			oSettings.iDisplayStart = 0;
			_fnCalculateEnd( oSettings );
			_fnDraw( oSettings );
			
			/* Rebuild search array 'offline' */
			_fnBuildSearchArray( oSettings, 0 );
		}
		
		
		/*
		 * Function: _fnFilterColumn
		 * Purpose:  Filter the table on a per-column basis
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 *           string:sInput - string to filter on
		 *           int:iColumn - column to filter
		 *           bool:bEscapeRegex - optional - default true
		 */
		function _fnFilterColumn ( oSettings, sInput, iColumn, bEscapeRegex )
		{
			if ( sInput == "" )
			{
				return;
			}
			
			var iIndexCorrector = 0;
			var sRegexMatch = bEscapeRegex ? _fnEscapeRegex( sInput ) : sInput;
			var rpSearch = new RegExp( sRegexMatch, "i" );
			
			for ( i=oSettings.aiDisplay.length-1 ; i>=0 ; i-- )
			{
				if ( ! rpSearch.test( oSettings.aoData[ oSettings.aiDisplay[i] ]._aData[iColumn] ) )
				{
					oSettings.aiDisplay.splice( i, 1 );
					iIndexCorrector++;
				}
			}
		}
		
		
		/*
		 * Function: _fnFilter
		 * Purpose:  Filter the data table based on user input and draw the table
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 *           string:sInput - string to filter on
		 *           int:iForce - optional - force a research of the master array (1) or not (undefined or 0)
		 *           bool:bEscapeRegex - optional - default true
		 */
		function _fnFilter( oSettings, sInput, iForce, bEscapeRegex )
		{
			var flag, i, j;
			
			/* Check if we are forcing or not - optional parameter */
			if ( typeof iForce == 'undefined' || iForce == null )
			{
				iForce = 0;
			}
			
			/* Check if we are re-drawing or not - optional parameter */
			if ( typeof bRedraw == 'undefined' || bRedraw == null )
			{
				bRedraw = true;
			}
			
			/* Generate the regular expression to use. Something along the lines of:
			 * ^(?=.*?\bone\b)(?=.*?\btwo\b)(?=.*?\bthree\b).*$
			 */
			var asSearch = bEscapeRegex ?
				_fnEscapeRegex( sInput ).split( ' ' ) :
				sInput.split( ' ' );
			var sRegExpString = '^(?=.*?'+asSearch.join( ')(?=.*?' )+').*$';
			var rpSearch = new RegExp( sRegExpString, "i" ); /* case insensitive */
			
			/*
			 * If the input is blank - we want the full data set
			 */
			if ( sInput.length <= 0 )
			{
				oSettings.aiDisplay.splice( 0, oSettings.aiDisplay.length);
				oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
				oSettings.sPreviousSearch = sInput;
			}
			else
			{
				/*
				 * We are starting a new search or the new search string is smaller 
				 * then the old one (i.e. delete). Search from the master array
			 	 */
				if ( oSettings.aiDisplay.length == oSettings.aiDisplayMaster.length ||
					   oSettings.sPreviousSearch.length > sInput.length || iForce == 1 )
				{
					/* Nuke the old display array - we are going to rebuild it */
					oSettings.aiDisplay.splice( 0, oSettings.aiDisplay.length);
					
					/* Force a rebuild of the search array */
					_fnBuildSearchArray( oSettings, 1 );
					
					/* Search through all records to populate the search array
					 * The the oSettings.aiDisplayMaster and asDataSearch arrays have 1 to 1 
					 * mapping
					 */
					for ( i=0 ; i<oSettings.aiDisplayMaster.length ; i++ )
					{
						if ( rpSearch.test(oSettings.asDataSearch[i]) )
						{
							oSettings.aiDisplay.push( oSettings.aiDisplayMaster[i] );
						}
					}
			  }
			  else
				{
			  	/* Using old search array - refine it - do it this way for speed
			  	 * Don't have to search the whole master array again
			 		 */
			  	var iIndexCorrector = 0;
			  	
			  	/* Search the current results */
			  	for ( i=0 ; i<oSettings.asDataSearch.length ; i++ )
					{
			  		if ( ! rpSearch.test(oSettings.asDataSearch[i]) )
						{
			  			oSettings.aiDisplay.splice( i-iIndexCorrector, 1 );
			  			iIndexCorrector++;
			  		}
			  	}
			  }
				
				oSettings.sPreviousSearch = sInput;
			}
		}
		
		
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * Sorting
		 */
		
		/*
	 	 * Function: _fnSort
		 * Purpose:  Change the order of the table
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 * Notes:    We always sort the master array and then apply a filter again
		 *   if it is needed. This probably isn't optimal - but atm I can't think
		 *   of any other way which is (each has disadvantages)
		 */
		_fnSort = function ( oSettings )
		{
			/*
			 * Funny one this - we want to sort aiDisplayMaster - but according to aoData[]._aData
			 *
			 * function _fnSortText ( a, b )
			 * {
			 * 	var iTest;
			 * 	var oSort = $.fn.dataTableExt.oSort;
			 * 	
			 * 	iTest = oSort['string-asc']( aoData[ a ]._aData[ COL ], aoData[ b ]._aData[ COL ] );
			 * 	if ( iTest == 0 )
			 * 		...
			 * }
			 */
			
			/* Here is what we are looking to achieve here (custom sort functions add complication...)
			 * function _fnSortText ( a, b )
			 * {
			 * 	var iTest;
			 *  var oSort = $.fn.dataTableExt.oSort;
			 * 	iTest = oSort['string-asc']( a[0], b[0] );
			 * 	if ( iTest == 0 )
			 * 		iTest = oSort['string-asc']( a[1], b[1] );
			 * 		if ( iTest == 0 )
			 * 			iTest = oSort['string-asc']( a[2], b[2] );
			 * 	
			 * 	return iTest;
			 * }
			 */
			var sDynamicSort = "var fnLocalSorting = function(a,b){"+
				"var iTest;"+
				"var oSort = $.fn.dataTableExt.oSort;"+
				"var aoData = oSettings.aoData;";
			var aaSort = oSettings.aaSorting;
			var iDataSort;
			var iDataType;
			
			for ( var i=0 ; i<aaSort.length-1 ; i++ )
			{
				iDataSort = oSettings.aoColumns[ aaSort[i][0] ].iDataSort;
				iDataType = oSettings.aoColumns[ iDataSort ].sType;
				sDynamicSort += "iTest = oSort['"+iDataType+"-"+aaSort[i][1]+"']"+
					"( aoData[a]._aData["+iDataSort+"], aoData[b]._aData["+iDataSort+"] ); if ( iTest == 0 )";
			}
			
			iDataSort = oSettings.aoColumns[ aaSort[aaSort.length-1][0] ].iDataSort;
			iDataType = oSettings.aoColumns[ iDataSort ].sType;
			sDynamicSort += "iTest = oSort['"+iDataType+"-"+aaSort[aaSort.length-1][1]+"']"+
				"( aoData[a]._aData["+iDataSort+"], aoData[b]._aData["+iDataSort+"] ); return iTest;}";
			
			/* The eval has to be done to a variable for IE */
			eval( sDynamicSort );
			oSettings.aiDisplayMaster.sort( fnLocalSorting );
			
			/* Copy the master data into the draw array and re-draw */
			if ( oSettings.oFeatures.bFilter && oSettings.sPreviousSearch != "" )
			{
				/* _fnFilter() will redraw the table for us */
				_fnFilterComplete( oSettings, oSettings.sPreviousSearch, 1 );
			}
			else
			{
				oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
				_fnCalculateEnd( oSettings );
				_fnDraw( oSettings );
			}
		}
		
		
		/*
		 * Function: _fnSortingClasses
		 * Purpose:  Set the sortting classes on the header
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnSortingClasses( oSettings )
		{
			$("thead th", oSettings.nTable).removeClass( "sorting_asc sorting_desc sorting" );
			var iCorrector = 0;
			
			/* Apply the required classes to the header */
			for ( var i=0 ; i<oSettings.aoColumns.length ; i++ )
			{
				if ( oSettings.aoColumns[i].bSortable && oSettings.aoColumns[i].bVisible )
				{
					var sClass = "sorting";
					for ( var j=0 ; j<oSettings.aaSorting.length ; j++ )
					{
						if ( oSettings.aaSorting[j][0] == i )
						{
							if ( oSettings.aaSorting[j][1] == "asc" )
								sClass = "sorting_asc";
							else
								sClass = "sorting_desc";
							break;
						}
					}
					$("thead th:eq("+_fnColumnIndexToVisible(oSettings, i)+")", oSettings.nTable).addClass( sClass );
				}
			}
			
			/* 
			 * Apply the required classes to the table body
			 * Note that this is given as a feature switch since it can significantly slow down a sort
			 * on large data sets (adding and removing of classes is always slow at the best of times..)
			 */
			if ( oSettings.oFeatures.bSortClasses )
			{
				var nTrs = _fnGetTrNodes( oSettings );
				$('td', nTrs).removeClass( 'sorting_1 sorting_2 sorting_3' );
				for ( var i=0 ; i<oSettings.aaSorting.length ; i++ )
				{
					/* Limit the number of classes to three */
					if ( i <= 1 )
					{
						$('td:eq('+_fnColumnIndexToVisible(oSettings, oSettings.aaSorting[i][0])+')', 
							nTrs).addClass( 'sorting_'+(i+1) );
					}
					else
					{
						$('td:eq('+_fnColumnIndexToVisible(oSettings, oSettings.aaSorting[i][0])+')', 
							nTrs).addClass( 'sorting_3' );
					}
				}
			}
		}
		
		
		/*
		 * Function: _fnVisibleToColumnIndex
		 * Purpose:  Covert the index of a visible column to the index in the data array (take account
		 *   of hidden columns)
		 * Returns:  int:i - the data index
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnVisibleToColumnIndex( oSettings, iMatch )
		{
			var iColumn = -1;
			
			for ( var i=0 ; i<oSettings.aoColumns.length ; i++ )
			{
				if ( oSettings.aoColumns[i].bVisible == true )
				{
					iColumn++;
				}
				
				if ( iColumn == iMatch )
				{
					return i;
				}
			}
			
			return null;
		}
		
		
		/*
		 * Function: _fnColumnIndexToVisible
		 * Purpose:  Covert the index of an index in the data array and convert it to the visible
		 *   column index (take account of hidden columns)
		 * Returns:  int:i - the data index
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnColumnIndexToVisible( oSettings, iMatch )
		{
			var iColumn = 0;
			
			for ( var i=0 ; i<oSettings.aoColumns.length ; i++ )
			{
				if ( i == iMatch )
				{
					return iColumn;
				}
				
				if ( oSettings.aoColumns[i].bVisible == true )
				{
					iColumn++;
				}
			}
			
			return null;
		}
		
		
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * Support functions
		 */
		
		/*
		 * Function: _fnBuildSearchArray
		 * Purpose:  Create an array which can be quickly search through
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 *           int:iMaster - use the master data array - optional
		 */
		function _fnBuildSearchArray ( oSettings, iMaster )
		{
			/* Clear out the old data */
			oSettings.asDataSearch.splice( 0, oSettings.asDataSearch.length );
			
			var aArray = (typeof iMaster != 'undefined' && iMaster == 1) ?
			 	oSettings.aiDisplayMaster : oSettings.aiDisplay;
			
			for ( i=0 ; i<aArray.length ; i++ )
			{
				oSettings.asDataSearch[i] = '';
				for ( j=0 ; j<oSettings.aoColumns.length ; j++ )
				{
					if ( oSettings.aoColumns[j].bSearchable )
					{
						var sData = oSettings.aoData[ aArray[i] ]._aData[j];
						
						if ( oSettings.aoColumns[j].sType == "html" )
						{
							oSettings.asDataSearch[i] += sData.replace(/\n/g," ").replace( /<.*?>/g, "" );+' ';
						}
						else if ( typeof sData == "string" )
						{
							oSettings.asDataSearch[i] += sData.replace(/\n/g," ")+' ';
						}
						else
						{
							oSettings.asDataSearch[i] += sData+' ';
						}
					}
				}
			}
		}
		
		
		/*
		 * Function: _fnCalculateEnd
		 * Purpose:  Rcalculate the end point based on the start point
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnCalculateEnd( oSettings )
		{
			if ( oSettings.oFeatures.bPaginate == false )
			{
				oSettings.iDisplayEnd = oSettings.aiDisplay.length;
			}
			else
			{
				/* Set the end point of the display - based on how many elements there are
				 * still to display
				 */
				if ( oSettings.iDisplayStart + oSettings.iDisplayLength > oSettings.aiDisplay.length )
					oSettings.iDisplayEnd = oSettings.aiDisplay.length;
				else
					oSettings.iDisplayEnd = oSettings.iDisplayStart + oSettings.iDisplayLength;
			}
		}
		
		
		/*
		 * Function: _fnConvertToWidth
		 * Purpose:  Convert a CSS unit width to pixels (e.g. 2em)
		 * Returns:  int:iWidth - width in pixels
		 * Inputs:   string:sWidth - width to be converted
		 *           node:nParent - parent to get the with for (required for
		 *             relative widths) - optional
		 */
		function _fnConvertToWidth ( sWidth, nParent )
		{
			if ( !sWidth || sWidth==null || sWidth=='' )
			{
				return 0;
			}
			
			if ( typeof nParent == "undefined" )
			{
				nParent = document.getElementsByTagName('body')[0];
			}
			
			var iWidth;
			var nTmp = document.createElement( "div" );
			nTmp.style.width = sWidth;
			
			nParent.appendChild( nTmp );
			iWidth = nTmp.offsetWidth;
			nParent.removeChild( nTmp );
			
			return ( iWidth );
		}
		
		
		/*
		 * Function: _fnCalculateColumnWidths
		 * Purpose:  Calculate the width of columns for the table
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnCalculateColumnWidths ( oSettings )
		{
			var iTableWidth = oSettings.nTable.offsetWidth;
			var iTotalUserIpSize = 0;
			var iTmpWidth;
			var iVisibleColumns = 0;
			var i;
			var oHeaders = $('thead th', oSettings.nTable);
			
			/* Convert any user input sizes into pixel sizes */
			for ( var i=0 ; i<oSettings.aoColumns.length ; i++ )
			{
				if ( oSettings.aoColumns[i].bVisible )
				{
					iVisibleColumns++;
					
					if ( oSettings.aoColumns[i].sWidth != null )
					{
						iTmpWidth = _fnConvertToWidth( oSettings.aoColumns[i].sWidth, 
							oSettings.nTable.parentNode );
						
						/* Total up the user defined widths for later calculations */
						iTotalUserIpSize += iTmpWidth;
						
						oSettings.aoColumns[i].sWidth = iTmpWidth+"px";
					}
				}
			}
			
			/* If the number of columns in the DOM equals the number that we
			 * have to process in dataTables, then we can use the offsets that are
			 * created by the web-browser. No custom sizes can be set in order for
			 * this to happen
			 */
			if ( oSettings.aoColumns.length == oHeaders.length && iTotalUserIpSize == 0 )
			{
				for ( i=0 ; i<oSettings.aoColumns.length ; i++ )
				{
					oSettings.aoColumns[i].sWidth = oHeaders[i].offsetWidth+"px";
				}
			}
			else
			{
				/* Otherwise we are going to have to do some calculations to get
				 * the width of each column. Construct a 1 row table with the maximum
				 * string sizes in the data, and any user defined widths
				 */
				var nCalcTmp = oSettings.nTable.cloneNode( false );
				nCalcTmp.setAttribute( "id", '' );
				
				var sTableTmp = '<table class="'+nCalcTmp.className+'">';
				var sCalcHead = "<tr>";
				var sCalcHtml = "<tr>";
				
				/* Construct a tempory table which we will inject (invisibly) into
				 * the dom - to let the browser do all the hard word
				 */
				for ( var i=0 ; i<oSettings.aoColumns.length ; i++ )
				{
					if ( oSettings.aoColumns[i].bVisible )
					{
						sCalcHead += '<th>'+oSettings.aoColumns[i].sTitle+'</th>';
						
						if ( oSettings.aoColumns[i].sWidth != null )
						{
							var sWidth = '';
							if ( oSettings.aoColumns[i].sWidth != null )
							{
								sWidth = ' style="width:'+oSettings.aoColumns[i].sWidth+';"';
							}
							
							sCalcHtml += '<td'+sWidth+' tag_index="'+i+'">'+fnGetMaxLenString( oSettings, i)+'</td>';
						}
						else
						{
							sCalcHtml += '<td tag_index="'+i+'">'+fnGetMaxLenString( oSettings, i)+'</td>';
						}
					}
				}
				
				sCalcHead += "</tr>";
				sCalcHtml += "</tr>";
				
				/* Create the tmp table node (thank you jQuery) */
				nCalcTmp = $( sTableTmp + sCalcHead + sCalcHtml +'</table>' )[0];
				nCalcTmp.style.width = iTableWidth + "px";
				nCalcTmp.style.visibility = "hidden";
				nCalcTmp.style.position = "absolute"; /* Try to aviod scroll bar */
				
				oSettings.nTable.parentNode.appendChild( nCalcTmp );
				
				var oNodes = $("td", nCalcTmp);
				var iIndex;
				
				/* Gather in the browser calculated widths for the rows */
				for ( i=0 ; i<oNodes.length ; i++ )
				{
					iIndex = oNodes[i].getAttribute('tag_index');
					
					oSettings.aoColumns[iIndex].sWidth = $("td", nCalcTmp)[i].offsetWidth +"px";
				}
				
				oSettings.nTable.parentNode.removeChild( nCalcTmp );
			}
		}
		
		
		/*
		 * Function: fnGetMaxLenString
		 * Purpose:  Get the maximum strlen for each data column
		 * Returns:  string: - max strlens for each column
		 * Inputs:   object:oSettings - dataTables settings object
		 *           int:iCol - column of interest
		 */
		function fnGetMaxLenString( oSettings, iCol )
		{
			var iMax = 0;
			var iMaxIndex = -1;
			
			for ( var i=0 ; i<oSettings.aoData.length ; i++ )
			{
				if ( oSettings.aoData[i]._aData[iCol].length > iMax )
				{
					iMax = oSettings.aoData[i]._aData[iCol].length;
					iMaxIndex = i;
				}
			}
			
			if ( iMaxIndex >= 0 )
				return oSettings.aoData[iMaxIndex]._aData[iCol];
			else
				return '';
		}
		
		
		/*
		 * Function: _fnArrayCmp
		 * Purpose:  Compare two arrays
		 * Returns:  0 if match, 1 if length is different, 2 if no match
		 * Inputs:   array:aArray1 - first array
		 *           array:aArray2 - second array
		 */
		function _fnArrayCmp( aArray1, aArray2 )
		{
			if ( aArray1.length != aArray2.length )
			{
				return 1;
			}
			
			for ( var i=0 ; i<aArray1.length ; i++ )
			{
				if ( aArray1[i] != aArray2[i] )
				{
					return 2;
				}
			}
			
			return 0;
		}
		
		
		/*
		 * Function: _fnMasterIndexFromDisplay
		 * Purpose:  Get the master index from the display index
		 * Returns:  int:i - index
		 * Inputs:   object:oSettings - dataTables settings object
		 *           int:iIndexAAData - display array index
		 */
		function _fnMasterIndexFromDisplay( oSettings, iIndexAAData )
		{
			var i = 0;
			
			while ( _fnArrayCmp( oSettings.aaDataMaster[i], oSettings.aaData[iIndexAAData] ) != 0 )
			{
				i++;
			}
			
			return i;
		}
		
		
		/*
		 * Function: _fnDetectType
		 * Purpose:  Get the sort type based on an input string
		 * Returns:  string: - type (defaults to 'string' if no type can be detected)
		 * Inputs:   string:sData - data we wish to know the type of
		 * Notes:    This function makes use of the DataTables plugin objct $.fn.dataTableExt 
		 *   (.aTypes) such that new types can easily be added.
		 */
		function _fnDetectType( sData )
		{
			var aTypes = $.fn.dataTableExt.aTypes;
			var iLen = aTypes.length;
			
			for ( var i=0 ; i<iLen ; i++ )
			{
				var sType = aTypes[i]( sData );
				if ( sType != null )
				{
					return sType;
				}
			}
			
			return 'string';
		}
		
		
		/*
		 * Function: _fnSettingsFromNode
		 * Purpose:  Return the settings object for a particular table
		 * Returns:  object: Settings object - or null if not found
		 * Inputs:   node:nTable - table we are using as a dataTable
		 */
		_fnSettingsFromNode = function( nTable )
		{
			for ( var i=0 ; i<_aoSettings.length ; i++ )
			{
				if ( _aoSettings[i].nTable == nTable )
				{
					return _aoSettings[i];
				}
			}
			
			return null;
		}
		
		
		/*
		 * Function: _fnGetDataMaster
		 * Purpose:  Return an array with the full table data
		 * Returns:  array array:aData - Master data array
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnGetDataMaster ( oSettings )
		{
			var aData = new Array();
			for ( var i=0 ; i<oSettings.aoData.length ; i++ )
			{
				aData.push( oSettings.aoData[i]._aData );
			}
			return aData;
		}
		
		
		/*
		 * Function: _fnGetTrNodes
		 * Purpose:  Return an array with the TR nodes for the table
		 * Returns:  array array:aData - TR array
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnGetTrNodes ( oSettings )
		{
			var aNodes = new Array();
			for ( var i=0 ; i<oSettings.aoData.length ; i++ )
			{
				aNodes.push( oSettings.aoData[i].nTr );
			}
			return aNodes;
		}
		
		
		/*
		 * Function: _fnEscapeRegex
		 * Purpose:  scape a string stuch that it can be used in a regular expression
		 * Returns:  string: - escaped string
		 * Inputs:   string:sVal - string to escape
		 */
		function _fnEscapeRegex ( sVal )
		{
			var acEscape = [ '/', '.', '*', '+', '?', '|', '(', ')', '[', ']', '{', '}', '\\' ];
		  var reReplace = new RegExp( '(\\' + acEscape.join('|\\') + ')', 'g' );
		  return sVal.replace(reReplace, '\\$1');
		}
		
		
		/*
		 * Function: _fnSaveState
		 * Purpose:  Save the state of a table in a cookie such that the page can be reloaded
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnSaveState ( oSettings )
		{
			if ( !oSettings.oFeatures.bStateSave )
			{
				return;
			}
			
			/* Store the interesting variables */
			var sValue = "{";
			sValue += '"iStart": '+oSettings.iDisplayStart+',';
			sValue += '"iEnd": '+oSettings.iDisplayEnd+',';
			sValue += '"iLength": '+oSettings.iDisplayLength+',';
			sValue += '"sFilter": "'+oSettings.sPreviousSearch.replace('"','\\"')+'",';
			sValue += '"aaSorting": [';
			
			for ( var i=0 ; i<oSettings.aaSorting.length ; i++ )
			{
				sValue += "["+oSettings.aaSorting[i][0]+",'"+oSettings.aaSorting[i][1]+"'],";
			}
			sValue = sValue.substring(0, sValue.length-1);
			sValue += "]}";
			
			_fnCreateCookie( "SpryMedia_DataTables_"+oSettings.sInstance, sValue, 
				oSettings.iCookieDuration );
		}
		
		
		/*
		 * Function: _fnLoadState
		 * Purpose:  Attempt to load a saved table state from a cookie
		 * Returns:  -
		 * Inputs:   object:oSettings - dataTables settings object
		 */
		function _fnLoadState ( oSettings )
		{
			if ( !oSettings.oFeatures.bStateSave )
			{
				return;
			}
			
			var sData = _fnReadCookie( "SpryMedia_DataTables_"+oSettings.sInstance );
			if ( sData != null && sData != '' )
			{
				oData = eval( '('+sData+')' );
				oSettings.iDisplayStart = oData.iStart;
				oSettings.iDisplayEnd = oData.iEnd;
				oSettings.iDisplayLength = oData.iLength;
				oSettings.sPreviousSearch = oData.sFilter;
				oSettings.aaSorting = oData.aaSorting.slice();
			}
		}
		
		
		/*
		 * Function: _fnCreateCookie
		 * Purpose:  Create a new cookie with a value to store the state of a table
		 * Returns:  -
		 * Inputs:   string:sName - name of the cookie to create
		 *           string:sValue - the value the cookie should take
		 *           int:iSecs - duration of the cookie
		 */
		function _fnCreateCookie ( sName, sValue, iSecs )
		{
			var date = new Date();
			date.setTime( date.getTime()+(iSecs*1000) );
			
			/* 
			 * Shocking but true - it would appear IE has major issues with having the path being
			 * set to anything but root. We need the cookie to be available based on the path, so we
			 * have to append the pathname to the cookie name. Appalling.
			 */
			sName += '_'+window.location.pathname;
			
			document.cookie = sName+"="+sValue+"; expires="+date.toGMTString()+"; path=/";
		}
		
		
		/*
		 * Function: _fnReadCookie
		 * Purpose:  Read an old cookie to get a cookie with an old table state
		 * Returns:  string: - contents of the cookie - or null if no cookie with that name found
		 * Inputs:   string:sName - name of the cookie to read
		 */
		function _fnReadCookie ( sName )
		{
			var sNameEQ = sName +'_'+ window.location.pathname + "=";
			var sCookieContents = document.cookie.split(';');
			
			for( var i=0 ; i<sCookieContents.length ; i++ )
			{
				var c = sCookieContents[i];
				
				while (c.charAt(0)==' ')
				{
					c = c.substring(1,c.length);
				}
				
				if (c.indexOf(sNameEQ) == 0)
				{
					return c.substring(sNameEQ.length,c.length);
				}
			}
			return null;
		}
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * API
		 * 
		 * I'm not overly happy with this solution - I'd much rather that there was a way of getting
		 * a list of all the private functions and do what we need to dynamically - but that doesn't
		 * appear to be possible. Bonkers. A better solution would be to provide a 'bind' type object.
		 */
		if ( bApi )
		{
			this.oApi['_fnInitalise'] = _fnInitalise;
			this.oApi['_fnLanguageProcess'] = _fnLanguageProcess;
			this.oApi['_fnAddColumn'] = _fnAddColumn;
			this.oApi['_fnAddData'] = _fnAddData;
			this.oApi['_fnGatherData'] = _fnGatherData;
			this.oApi['_fnDrawHead'] = _fnDrawHead;
			this.oApi['_fnDraw'] = _fnDraw;
			this.oApi['_fnAddOptionsHtml'] = _fnAddOptionsHtml;
			this.oApi['_fnFeatureHtmlFilter'] = _fnFeatureHtmlFilter;
			this.oApi['_fnFeatureHtmlInfo'] = _fnFeatureHtmlInfo;
			this.oApi['_fnFeatureHtmlPaginate'] = _fnFeatureHtmlPaginate;
			this.oApi['_fnFeatureHtmlLength'] = _fnFeatureHtmlLength;
			this.oApi['_fnFeatureHtmlProcessing'] = _fnFeatureHtmlProcessing;
			this.oApi['_fnProcessingDisplay'] = _fnProcessingDisplay;
			this.oApi['_fnFilterComplete'] = _fnFilterComplete;
			this.oApi['_fnFilterColumn'] = _fnFilterColumn;
			this.oApi['_fnFilter'] = _fnFilter;
			this.oApi['_fnSortingClasses'] = _fnSortingClasses;
			this.oApi['_fnVisibleToColumnIndex'] = _fnVisibleToColumnIndex;
			this.oApi['_fnColumnIndexToVisible'] = _fnColumnIndexToVisible;
			this.oApi['_fnBuildSearchArray'] = _fnBuildSearchArray;
			this.oApi['_fnCalculateEnd'] = _fnCalculateEnd;
			this.oApi['_fnConvertToWidth'] = _fnConvertToWidth;
			this.oApi['_fnCalculateColumnWidths'] = _fnCalculateColumnWidths;
			this.oApi['_fnArrayCmp'] = _fnArrayCmp;
			this.oApi['_fnMasterIndexFromDisplay'] = _fnMasterIndexFromDisplay;
			this.oApi['_fnDetectType'] = _fnDetectType;
			this.oApi['_fnGetDataMaster'] = _fnGetDataMaster;
			this.oApi['_fnGetTrNodes'] = _fnGetTrNodes;
			this.oApi['_fnEscapeRegex'] = _fnEscapeRegex;
			this.oApi['_fnSaveState'] = _fnSaveState;
			this.oApi['_fnLoadState'] = _fnLoadState;
			this.oApi['_fnCreateCookie'] = _fnCreateCookie;
			this.oApi['_fnReadCookie'] = _fnReadCookie;
		}
		
		
		/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
		 * Constructor
		 */
		return this.each(function()
		{
			/* Make a complete and independent copy of the settings object */
			var oSettings = new classSettings()
			_aoSettings.push( oSettings );
			
			var bInitHandedOff = false;
			var bUsePassedData = false;
			
			/* Set the id */
			var sId = this.getAttribute( 'id' );
			if ( sId != null )
			{
				oSettings.sTableId = sId;
				oSettings.sInstance = sId;
			}
			else
			{
				oSettings.sInstance = $.fn.dataTableExt._oExternConfig.iNextUnique ++;
			}
			
			/* Set the table node */
			oSettings.nTable = this;
			
			/* Store the features that we have available */
			if ( typeof oInit != 'undefined' && oInit != null )
			{
				if ( typeof oInit.bPaginate != 'undefined' )
					oSettings.oFeatures.bPaginate = oInit.bPaginate;
				
				if ( typeof oInit.bLengthChange != 'undefined' )
					oSettings.oFeatures.bLengthChange = oInit.bLengthChange;
				
				if ( typeof oInit.bFilter != 'undefined' )
					oSettings.oFeatures.bFilter = oInit.bFilter;
				
				if ( typeof oInit.bSort != 'undefined' )
					oSettings.oFeatures.bSort = oInit.bSort;
				
				if ( typeof oInit.bInfo != 'undefined' )
					oSettings.oFeatures.bInfo = oInit.bInfo;
				
				if ( typeof oInit.bProcessing != 'undefined' )
					oSettings.oFeatures.bProcessing = oInit.bProcessing;
				
				if ( typeof oInit.bAutoWidth != 'undefined' )
					oSettings.oFeatures.bAutoWidth = oInit.bAutoWidth;
				
				if ( typeof oInit.bSortClasses != 'undefined' )
					oSettings.oFeatures.bSortClasses = oInit.bSortClasses;
				
				if ( typeof oInit.bStateSave != 'undefined' )
				{
					oSettings.oFeatures.bStateSave = oInit.bStateSave;
					_fnLoadState( oSettings );
				}
				
				if ( typeof oInit.aaData != 'undefined' )
					bUsePassedData = true;
				
				if ( typeof oInit.iDisplayLength != 'undefined' )
					oSettings.iDisplayLength = oInit.iDisplayLength;
				
				if ( typeof oInit.asStripClasses != 'undefined' )
					oSettings.asStripClasses = oInit.asStripClasses;
				
				if ( typeof oInit.fnRowCallback != 'undefined' )
					oSettings.fnRowCallback = oInit.fnRowCallback;
				
				if ( typeof oInit.fnHeaderCallback != 'undefined' )
					oSettings.fnHeaderCallback = oInit.fnHeaderCallback;
				
				if ( typeof oInit.fnFooterCallback != 'undefined' )
					oSettings.fnFooterCallback = oInit.fnFooterCallback;
				
				if ( typeof oInit.fnDrawCallback != 'undefined' )
					oSettings.fnDrawCallback = oInit.fnDrawCallback;
				
				if ( typeof oInit.fnInitComplete != 'undefined' )
					oSettings.fnInitComplete = oInit.fnInitComplete;
				
				if ( typeof oInit.aaSorting != 'undefined' )
					oSettings.aaSorting = oInit.aaSorting;
				
				if ( typeof oInit.sPaginationType != 'undefined' )
					oSettings.sPaginationType = oInit.sPaginationType;
				
				if ( typeof oInit.sDom != 'undefined' )
					oSettings.sDomPositioning = oInit.sDom;
				
				if ( typeof oInit.sAjaxSource != 'undefined' )
					oSettings.sAjaxSource = oInit.sAjaxSource;
				
				if ( typeof oInit.iCookieDuration != 'undefined' )
					oSettings.iCookieDuration = oInit.iCookieDuration;
				
				/* Backwards compatability */
				/* aoColumns / aoData - remove at some point... */
				if ( typeof oInit != 'undefined' && typeof oInit.aoData != 'undefined' )
				{
					oInit.aoColumns = oInit.aoData;
				}
				
				/* Language definitions */
				if ( typeof oInit.oLanguage != 'undefined' )
				{
					bInitHandedOff = true;
					
					if ( typeof oInit.oLanguage.sUrl != 'undefined' )
					{
						/* Get the language definitions from a file */
						oSettings.oLanguage.sUrl = oInit.oLanguage.sUrl;
						$.getJSON( oSettings.oLanguage.sUrl, null, function( json ) { 
							_fnLanguageProcess( oSettings, json ) } );
					}
					else
					{
						_fnLanguageProcess( oSettings, oInit.oLanguage );
					}
				}
				/* Warning: The _fnLanguageProcess function is async to the remainder of this function due
				 * to the XHR. We use _bInitialised in _fnLanguageProcess() to check this the processing 
				 * below is complete. The reason for spliting it like this is optimisation - we can fire
				 * off the XHR (if needed) and then continue processing the data.
				 */
			}
			
			/* See if we should load columns automatically or use defined ones */
			if ( typeof oInit != 'undefined' && typeof oInit.aoColumns != 'undefined' )
			{
				for ( var i=0 ; i<oInit.aoColumns.length ; i++ )
				{
					_fnAddColumn( oSettings, oInit.aoColumns[i] );
				}
			}
			else
			{
				$('thead th', this).each( function() { _fnAddColumn( oSettings, null ) } );
			}
			
			/* Check if there is data passing into the constructor */
			if ( bUsePassedData )
			{
				for ( var i=0 ; i<oInit.aaData.length ; i++ )
				{
					_fnAddData( oSettings, oInit.aaData[ i ] );
				}
				
				if ( $('thead', this).length == 0 )
				{
			 		this.appendChild( document.createElement( 'thead' ) );
				}
				
				if ( $('tbody', this).length == 0 )
				{
			 		this.appendChild( document.createElement( 'tbody' ) );
				}
			}
			else
			{
				/* Grab the data from the page */
				_fnGatherData( oSettings );
			}
			
			/* Copy the data index array */
			oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
			
			/* Calculate sizes for columns */
			if ( oSettings.oFeatures.bAutoWidth )
			{
				_fnCalculateColumnWidths( oSettings );
			}
			
			/* Initialisation complete - table can be drawn */
			oSettings.bInitialised = true;
			
			/* Check if we need to initialise the table (it might not have been handed off to the
			 * language processor)
			 */
			if ( bInitHandedOff == false )
			{
				_fnInitalise( oSettings )
			}
		});
	}
})(jQuery);
