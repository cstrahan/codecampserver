
dojo.provide("dojo.widget.Toggler");dojo.require("dojo.widget.*");dojo.require("dojo.event.*");dojo.widget.defineWidget("dojo.widget.Toggler",dojo.widget.HtmlWidget,{targetId:"",fillInTemplate:function(){dojo.event.connect(this.domNode,"onclick",this,"onClick");},onClick:function(){var pane=dojo.widget.byId(this.targetId);if(!pane){return;}
pane.explodeSrc=this.domNode;pane.toggleShowing();}});