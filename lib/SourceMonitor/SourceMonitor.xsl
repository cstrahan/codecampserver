<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/TR/xhtml1/strict">
  <xsl:output method="html"/>
  
  <xsl:template match="/">
    <xsl:apply-templates select="//SourceMonitorComplexitySummary" />					
  </xsl:template>
  
 <!-- The most complex methods -->
  <xsl:template match="SourceMonitorComplexitySummary">
    <table class="section-table" cellpadding="2" cellspacing="0" border="0" width="98%">
      <colgroup>
        <col width="300px" />
        <col width="50px" />
        <col />
        <col width="50px" />
      </colgroup>
      <tr>
        <td class="sectionheader" colspan="4">
           SourceMonitor - <xsl:value-of select="count(MostComplexMethods/Method)" /> Most Complex Methods
        </td>
      </tr>
      <tr>
        <td class="header-label">
          Method
        </td>
        <td class="header-label" align="center">
          Complexity
        </td>
        <td class="header-label">
          File
        </td>
        <td class="header-label">
          Line
        </td>
      </tr>
      <xsl:apply-templates select="MostComplexMethods/Method" />
    </table>
    
    <br/>
    
    <!-- The most deeply nested code blocks -->
    <table class="section-table" cellpadding="2" cellspacing="0" border="0" width="98%">
      <colgroup>
        <col width="50px"/>
        <col />
        <col width="50px" />
      </colgroup>
      <tr>
        <td class="sectionheader" colspan="4">
           SourceMonitor - <xsl:value-of select="count(MostDeeplyNestedCode/Block)" /> Most Deeply Nested Code Blocks
        </td>
      </tr>
      <tr>
        <td class="header-label">
          Depth
        </td>
        <td class="header-label">
          File
        </td>
        <td class="header-label">
          Line
        </td>
      </tr>
      <xsl:apply-templates select="MostDeeplyNestedCode/Block" />
    </table>
  </xsl:template>

  <!-- Complex Method List -->
  <xsl:template match="MostComplexMethods/Method">
      <tr>
        <xsl:if test="position() mod 2 = 1">
          <xsl:attribute name="class">section-oddrow</xsl:attribute>
        </xsl:if>
        <td>
          <xsl:value-of select="Name"/>
        </td>
        <td align="center">
          <xsl:value-of select="Complexity" />
        </td>
        <td >
          <xsl:value-of select="File" />
        </td>
        <td align="right">
          <xsl:value-of select="Line" />
        </td>
      </tr>
  </xsl:template>

  <!-- Deeply Nested Blocks List -->
  <xsl:template match="MostDeeplyNestedCode/Block">
      <tr>
        <xsl:if test="position() mod 2 = 1">
          <xsl:attribute name="class">section-oddrow</xsl:attribute>
        </xsl:if>
        <td>
          <xsl:value-of select="Depth" />
        </td>
        <td>
          <xsl:value-of select="File" />
        </td>
        <td align="right">
          <xsl:value-of select="Line" />
        </td>
      </tr>
  </xsl:template>

</xsl:stylesheet>
