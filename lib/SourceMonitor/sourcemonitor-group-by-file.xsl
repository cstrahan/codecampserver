<?xml version="1.0"?>

<!-- group_by_file.xsl                                                                   -->
<!--                                                                                     -->
<!-- Abstract:                                                                           --> 
<!--    Formats SourceMonitor "detailed" or "summary" XML output in JUnit report style.  -->
<!--    Shows one table per file listing all metrics.                                    -->
<!--                                                                                     -->
<!-- Parameters:                                                                         --> 
<!--    None.                                                                            -->
<!--                                                                                     -->
<!-- History:                                                                            -->
<!--    1.0 - 17/07/04, Y. Coene                                                         --> 
<!--        Original with CSS part derived from JUnit stylesheet.                        -->
<!--                                                                                     -->
<!--    - 17/08/05, R. Curry                                                             -->
<!--        Modified for use with CruiseControl.NET integration.                         -->

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html"/>
  <xsl:template match="/">
        <style type="text/css">
          .sm body {
          font:normal 88% verdana,arial,helvetica;
          color:#000000;
          }
          .sm table tr td, tr th {
            font-size: 88%;
          }
          .sm table.details tr th{
          font-weight: bold;
          text-align:left;
          background:#a6caf0;
          }
          .sm table.details tr td{
          background:#eeeee0;
          }
          
          .sm p {
          line-height:1.5em;
          margin-top:0.5em; margin-bottom:1.0em;
          margin-left:2em;
          margin-right:2em;
          }
          .sm h1 {
          margin: 0px 0px 5px; font: 165% verdana,arial,helvetica
          }
          .sm h2 {
          margin-top: 1em; margin-bottom: 0.5em; font: bold 125% verdana,arial,helvetica
          }
          .sm h3 {
          margin-bottom: 0.5em; font: bold 115% verdana,arial,helvetica
          }
          .sm h4 {
          margin-bottom: 0.5em; font: bold 100% verdana,arial,helvetica
          }
          .sm h5 {
          margin-bottom: 0.5em; font: bold 100% verdana,arial,helvetica
          }
          .sm h6 {
          margin-bottom: 0.5em; font: bold 100% verdana,arial,helvetica
          }
          .Error {
          font-weight:bold; color:red;
          }
          .Failure {
          font-weight:bold; color:purple;
          }
          .Properties {
          text-align:right;
          }
        </style>
        <div class="sm">
        <h1>SourceMonitor Analysis</h1>
        <p align="right">Designed for use with <a href="http://www.campwoodsw.com/sourcemonitor.html">SourceMonitor</a> and <a href="http://ccnet.thoughtworks.com">CruiseControl.NET</a>.</p>
        <hr size="2"/>
        <xsl:apply-templates/>
        </div>
  </xsl:template>
  
  <xsl:template match="file">
    <h2>File <xsl:value-of select="@file_name"/></h2>
    <table class="details" border="0" width="100%">
      <tr>
        <th width="10%">ID</th>
        <th width="60%">Metric</th>
        <th>Value</th>
      </tr>
      <xsl:apply-templates select="metrics/metric"/>
    </table>
    <p/>
    
  </xsl:template>
  
  <xsl:template match="checkpoint/metrics">
      <h2>Summary</h2>
      <table class="details" border="0" width="100%">
        <tr>
          <th width="10%">ID</th>
          <th width="60%">Metric</th>
          <th>Value</th>
        </tr>
        <xsl:apply-templates select="../metrics/metric"/>
      </table>
      <p/>
      
  </xsl:template>


  <xsl:template match="metric">
    <xsl:variable name="myref"><xsl:value-of select="@id"/></xsl:variable>

    <tr>
      <td><xsl:value-of select="@id"/></td>
      <td><xsl:value-of select="//metric_name[@id=$myref]"/></td>
      <td><xsl:value-of select="."/></td>
    </tr>
  </xsl:template>


  <xsl:template match="text()"/>
  
  
  

</xsl:stylesheet>
