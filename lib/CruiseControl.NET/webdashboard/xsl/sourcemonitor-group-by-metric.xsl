<?xml version="1.0"?>

<!-- group_by_metric.xsl                                                                 -->
<!--                                                                                     -->
<!-- Abstract:                                                                           --> 
<!--    Formats SourceMonitor "detailed" XML output in JUnit report style.               -->
<!--    Shows one table per metric listing all files.                                    -->
<!--                                                                                     -->
<!-- Parameters:                                                                         --> 
<!--    Optional parameter "metric" can be used to limit output to a single metric.      -->
<!--    E.g. use parameter 'metric="M12"' to limit report to metric M12.                 -->
<!--                                                                                     -->
<!-- History:                                                                            -->
<!--    1.0 - 17/07/04, Y. Coene                                                         --> 
<!--        Original with CSS part derived from JUnit stylesheet.                        -->
<!--                                                                                     -->
<!--    - 17/08/05, R. Curry                                                             -->
<!--        Modified for use with CruiseControl.NET integration.                         -->

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:param name="metric">all</xsl:param>
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
  
  <xsl:template match="metric_name">
  
    <xsl:if test="$metric = @id or $metric = 'all'">
  
      <h2>Metric <xsl:value-of select="@id"/> - <xsl:value-of select="text()"/></h2>
      <table class="details" border="0" width="100%">
        <tr>
          <th width="10%">#</th>
          <th width="60%">File</th>
          <th>Value</th>
        </tr>
      
        <xsl:variable name="metric_id"><xsl:value-of select="@id"/></xsl:variable>
      
        <xsl:for-each select="//file/metrics/metric[@id=$metric_id]">                        
          <tr>             
          <td><xsl:value-of select="position()"/></td> 
          <td><xsl:value-of select="../../@file_name"/></td>
          <td><xsl:value-of select="."/></td>
          </tr>  
        </xsl:for-each>
      </table>
      <p/>
      
    </xsl:if>
    
  </xsl:template>


  <xsl:template match="text()"/>
  
  
  

</xsl:stylesheet>
