/**
 * Technosophos RSS Widget
 * Copyright (c) 2007 Technosophos/Aleph-Null, Inc.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.  
 *
 * ============
 * Typical Use:
 * Technosophos.rssWidget("http://myfeed/rss.xml", "#my-target-div");
 *
 * NOTE: Due to the browser security model, the RSS URL must reside on the
 * same server as HTML page into which this widget is placed.
 *
 * This script requires jQuery 1.2 or greater. http://jquery.com
 *
 * @author Matt Butcher <mbutcher@aleph-null.tv>
 */
 
var Technosophos = Technosophos || {widgets:{},settings:{}};

jQuery.extend( Technosophos, {
	shorten: function(myString, maxlen, sep) {
		var s = sep || '...';
		var m = maxlen || 256;
		if ( myString.length <= m ) return myString
		 
		myString = myString.substring(0, maxlen - s.length);
		var l = myString.lastIndexOf(' ');
		return (l < 0 ? myString : myString.substring(0, l)) + s;
	},
	rssWidget: function(myFeed, myTarget) {
		jQuery().ready(function() {
			Technosophos.widgets.rss.create(myFeed, myTarget);
		});
	}
});

jQuery.extend(Technosophos.widgets, {
	rss: {
		settings: {
			feed: "http://aleph-null.tv", // FEED URL
			baseURI: "/", // RELATIVE PATH FOR LOADING IMAGES
			backgroundImage: "background-ani230x300.png", // BG IMAGE
			target: "#technosophos-rss-widget", // ELEMENT TO INSERT WIDGET INTO
			refreshInterval: 7000, // HOW LONG TEXT IS DISPLAYED
			refreshDuration: "slow", // HOW LONG A FADE EFFECT SHOULD TAKE
			maxTextLength: 192, // MAX NUM OF CHARS TO DISPLAY IN DESCRIPTION
			advertise: false // If TRUE, this will advertise for the widget.
		},
		create: function(myfeed, mytarget) {
			var _rss = this;	
			if(myfeed) _rss.settings.feed = myfeed;
			if(mytarget) _rss.settings.target = mytarget;
			
			_rss.insertWidget();
			var res = jQuery.get(this.settings.feed, function(data) {
				//alert("Data: " +data);
				_rss.feedContents = data;
				_rss.title = jQuery(Technosophos.widgets.rss.feedContents).find("channel > title").text();
				_rss.link = jQuery(Technosophos.widgets.rss.feedContents).find("channel > link").text();
				_rss.items = jQuery.makeArray(jQuery(Technosophos.widgets.rss.feedContents).find("item"));
				jQuery(_rss.settings.target)
					.find(".technosophos-rss-widget-inner-div")
					.html("<div class='technosophos-rss-widget-title'>"
						+ _rss.title
						+ "</div>"
						+ "<div class='technosophos-rss-widget-item'></div>"
						+ "<a href='"
						+ _rss.link
						+"'>"
						+"<img src='/images/icons/feed.png'/>"
						+"</a>"
						
					);
				_rss.periodicRefreshWidget();
			});
		},
		insertWidget: function() {
			jQuery("head").append(
				'<style>'
				+Technosophos.widgets.rss.settings.target
				+' div.technosophos-rss-widget-outer-div { '
				+ 'width: 230px; height: 300px;'
				+ 'background-image: url("'
				+Technosophos.widgets.rss.settings.baseURI
				+'images/'
				+Technosophos.widgets.rss.settings.backgroundImage
				+'");'
				+ ' }'
				+Technosophos.widgets.rss.settings.target
				+' div.technosophos-rss-widget-inner-div { margin: 5px; padding-top: 12px;}'
				+Technosophos.widgets.rss.settings.target
				+' div.technosophos-rss-widget-item { font-size: 10pt; }'
				+Technosophos.widgets.rss.settings.target
				+' div.technosophos-rss-widget-title {font-size:12pt; font-weight:bold; text-align:center; color:#efefef; padding-bottom:10px;}'
				+Technosophos.widgets.rss.settings.target
				+' div.technosophs-rss-widget-item-title > a {color:#800019;text-decoration:none;font-weight:bold}'
				+Technosophos.widgets.rss.settings.target
				+' div.technosophs-rss-widget-item-desc {}'
				+'</style>'
			);
			jQuery(Technosophos.widgets.rss.settings.target).html(
				"<div class='technosophos-rss-widget-outer-div'>"
				+"<div class='technosophos-rss-widget-inner-div'>"
				+ "Loading..."
				+"</div></div>"
			);
			//this.refreshWidget();
		},
		pauseRefresh: function() {
			clearInterval(Technosophos.widgets.rss.intID);
		},
		unpauseRefresh: function() {this.periodicRefreshWidget()},
		periodicRefreshWidget: function() {
			Technosophos.widgets.rss.refreshWidget(); // do one right now
			Technosophos.widgets.rss.intID = setInterval(
				"Technosophos.widgets.rss.refreshWidget()", 
				Technosophos.widgets.rss.settings.refreshInterval
			); 
		},
		refreshWidget: function() {
			var _rss = this;
			//alert(title);
			jQuery(Technosophos.widgets.rss.settings.target)
				.find(".technosophos-rss-widget-item")
				.fadeOut(Technosophos.widgets.rss.settings.refreshDuration, _rss.changeItem);
		},
		changeItem: function() {
			var _rss = Technosophos.widgets.rss;
			if(_rss.items.length == 0) return; // Do nothing if no content.
			_rss.c < _rss.items.length -1 ? _rss.c++: _rss.c = 0; 
			var item = _rss.items[_rss.c] || {title:"No Content"};
			var j = jQuery(item);
			var l = j.find('link').text();
			if( l.indexOf('javascript') == 0 ) l = "#";
			var t = j.find('title').text();
			var d = Technosophos.shorten(j.find('description').text(), _rss.settings.maxTextLength)
			jQuery(this).html(
				"<div class='technosophs-rss-widget-item-title'><a href='"
				+ l
				+"'>"
				+ t
				+"</a></div><div class='technosophs-rss-widget-item-desc'>"
				+ d
				//+ "<br/><a href='"+l+"'>[More]</a>"
				+"</div>"
			).fadeIn(_rss.settings.refreshDuration);
		},
		// More or less private vars:
		title: "No Feed Found",
		items: [],//{title:"Get Some Content",description:""}],
		c: 0,
		feedContents: null
	}
}); 

 