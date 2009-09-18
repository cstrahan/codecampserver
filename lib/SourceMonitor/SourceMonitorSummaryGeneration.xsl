<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <!--- SourceMonitor Summary Xml File Generation created by Eden Ridgway -->
  <xsl:output method="xml"/>
  
  <xsl:template match="/">
    <xsl:apply-templates select="/sourcemonitor_metrics" />
  </xsl:template>
  
  <!-- Transform the results into a simpler more intuitive and summarised format -->
  <xsl:template match="sourcemonitor_metrics">
      <SourceMonitorComplexitySummary>
        <MostComplexMethods>
          <xsl:call-template name="MostComplexMethods"/>
        </MostComplexMethods>
        
        <LinePerMethod>
          <xsl:call-template name="LinesPerMethod"/>
        </LinePerMethod>

        <MostDeeplyNestedCode>
          <xsl:call-template name="MostDeeplyNestedCode"/>
        </MostDeeplyNestedCode>
        
      </SourceMonitorComplexitySummary>
  </xsl:template>

  <!-- Complexity Metrics -->
  <xsl:template name="MostComplexMethods">
    <xsl:for-each select=".//file">
      <xsl:sort select="metrics/metric[@id='M10']" order="descending" data-type="number" />
      
      <xsl:choose>
        <xsl:when test="position() &lt; 16">
           <Method>
              <File><xsl:value-of select="@file_name"/></File>
              <Name><xsl:value-of select="metrics/metric[@id='M9']"/></Name>
              <Line><xsl:value-of select="metrics/metric[@id='M8']"/></Line>
              <Complexity><xsl:value-of select="metrics/metric[@id='M10']"/></Complexity>
           </Method>
        </xsl:when>
      </xsl:choose>
    </xsl:for-each>
  </xsl:template>

  <!-- Complexity Metrics -->
  <xsl:template name="LinesPerMethod">
    <xsl:for-each select=".//file">
      <xsl:sort select="metrics/metric[@id='M5']" order="descending" data-type="number" />
      
      <xsl:choose>
        <xsl:when test="position() &lt; 16">
           <Method>
              <File><xsl:value-of select="@file_name"/></File>
              <Lines><xsl:value-of select="metrics/metric[@id='M5']"/></Lines>
           </Method>
        </xsl:when>
      </xsl:choose>
    </xsl:for-each>
  </xsl:template>

  <!-- Nesting Metrics -->
  <xsl:template name="MostDeeplyNestedCode">
    <xsl:for-each select=".//file">
        <xsl:sort select="metrics/metric[@id='M12']" order="descending" data-type="text" />
      
        <xsl:choose>
          <xsl:when test="position() &lt; 16">
             <Block>
                <File><xsl:value-of select="@file_name"/></File>
                <Depth><xsl:value-of select="metrics/metric[@id='M12']"/></Depth>
                <Line><xsl:value-of select="metrics/metric[@id='M11']"/></Line>
             </Block>
          </xsl:when>
        </xsl:choose>
    </xsl:for-each>
  </xsl:template>
  
</xsl:stylesheet>
