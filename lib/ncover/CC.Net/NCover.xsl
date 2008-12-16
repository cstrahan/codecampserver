<xsl:stylesheet version="1.0" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns="http://www.w3.org/TR/xhtml1/strict">
  <xsl:output method="html"/>
  <!-- saved from url=(0022)http://www.ncover.org/ -->
  <!-- created by Yves Lorphelin, largely inspired by the nunitsumary.xsl (see nantcontrib.sourceforge.net)-->
	<xsl:template match="/">
		<html>
			<head>
				<title>NCover Code Coverage Report</title>
				<style>
          BODY {
          font: small verdana, arial, helvetica;
          color:#000000;
          }

          P {
          line-height:1.5em;
          margin-top:0.5em; margin-bottom:1.0em;
          }
          H1 {
          MARGIN: 0px 0px 5px;
          FONT: bold larger arial, verdana, helvetica; color: Black

          }
          H2 {
          MARGIN-TOP: 1em; MARGIN-BOTTOM: 0.5em;
          FONT: larger verdana,arial,helvetica; color: Black
          }
          H3 {
          MARGIN-BOTTOM: 0.5em; FONT: bold 13px verdana,arial,helvetica
          }
          H4 {
          MARGIN-BOTTOM: 0.5em; FONT: bold 100% verdana,arial,helvetica
          }
          H5 {
          MARGIN-BOTTOM: 0.5em; FONT: bold 100% verdana,arial,helvetica
          }
          H6 {
          MARGIN-BOTTOM: 0.5em; FONT: bold 100% verdana,arial,helvetica
          }
          .notVisited { background:red; }
          .excluded { background: skyblue; }
          .visited { background: #90ee90; }
          .title { font-size: 12px; font-weight: bold; }
          .assembly { font-size: normal; font-weight: bold; font-size: 11px; color: Black}
          .class {font-size:normal; cursor: hand; color: #444444; font-size: 11px}
          .module { color: navy; font-size: 12px; }
          .method {cursor: hand; color: ; font-size: 10px; font-weight: bold; }
          .subtitle { color: black; font-size: 10px; font-weight: bold; }
          .hdrcell  {font-size:9px; background-color: #DDEEFF; }
          .datacell {font-size:9px; background-color: #FFFFEE; text-align: right; }
          .hldatacell {font-size:9px; background-color: #FFCCCC; text-align: right; }
          .exdatacell {font-size:9px; background-color: #DDEEFF; text-align: right; }
          .detailPercent {  font-size: 9px; font-weight: bold; padding-top: 1px; padding-bottom: 1px; padding-left: 3px; padding-right: 3px; color: Black}
          .collapse {}
        </style>
				<script>
          function toggle (field)
          {
            var block = document.getElementById(field);
            if (block.style.display == 'none')
            {
              block.style.display = 'block';
            }
            else
            {
              block.style.display = 'none';
            }
          }

          <!--function SwitchAll(how)
          {
            var len = document.all.length-1;
            for(i=0; i!=len; i++)
            {
              var block = document.all[i];
              --><!--if (block != null && block.id != '')
						  {--><!-- 
                block.style.display=how;
              --><!--}--><!--
					  }
				  }-->

          function SwitchAll(how)
          {
              var length = document.all.length-1;
              for(i=0; i != length; i++)
              {
                var block = document.all[i];
                if(block.className == 'collapse')
                {
                    if (block.style.display == 'none' ||
                      block.style.display == 'block')
                    {
                      block.style.display=how;
                    }
                }
              }
          }

          function ExpandAll()
          {
          SwitchAll('block');
          }

          function CollapseAll()
          {
          SwitchAll('none');
          }
        </script>
			</head>
			<body>
				<a name="#top"></a>
				<xsl:call-template name="header" />
				<xsl:call-template name="documents" />
				<xsl:call-template name="ModuleSummary" />
				<xsl:call-template name="module" />
				<xsl:call-template name="footer" />
				<script language="JavaScript">CollapseAll();</script>
			</body>
		</html>
	</xsl:template>
	<xsl:template name="documents">
		<xsl:for-each select="//documents/doc[@excluded='false']">
			<xsl:variable name="LongName" select="./@url" />
		</xsl:for-each>
	</xsl:template>
		<!-- Modules Summary -->
	<xsl:template name="ModuleSummary">
    <table>
      <tr>
        <td>
		      <H2>Modules summary</H2>
        </td>
      </tr>
    </table>
		<xsl:for-each select="//module">
			<xsl:sort select="@assembly" />
			<table width='90%'>
				<tr>
					<xsl:call-template name="ModuleSummaryDetail">
						<xsl:with-param name="module" select="./@assembly" />
					</xsl:call-template>
				</tr>
			</table>
		</xsl:for-each>
		<hr size="1" />
	</xsl:template>
	<xsl:template name="ModuleSummaryDetail">
		<xsl:param name="module" />
		<xsl:variable name="total" select="count(./class/method/seqpnt[@ex='false'])" />
		<xsl:variable name="notVisited" select="count( ./class/method/seqpnt[ @vc='0' ][ @ex='false' ])" />
		<xsl:variable name="visited" select="count(./class/method/seqpnt[not(@vc='0')][ @ex='false' ])" />
		<td width="30%">
			<div class="assembly">
				<a href="#{generate-id($module)}">
					<xsl:value-of select="$module" />
				</a>
			</div>
		</td>
		<td width="70%">
			<xsl:call-template name="detailPercent">
				<xsl:with-param name="visited" select="$visited" />
				<xsl:with-param name="notVisited" select="$notVisited" />
				<xsl:with-param name="total" select="$total" />
			</xsl:call-template>
		</td>
	</xsl:template>
	<!-- draw % table-->
	<xsl:template name="detailPercent">
		<xsl:param name="visited" />
		<xsl:param name="notVisited" />
		<xsl:param name="total" />
		<table width="100%" class="detailPercent">
			<tr>
			<xsl:if test="($notVisited=0) and ($visited=0)">
				<td class="excluded" width="100%">Excluded</td>
			</xsl:if>
			<xsl:if test="not($notVisited=0)">
					<td class="notVisited">
						<xsl:attribute name="width">
							<xsl:value-of select="concat($notVisited div $total * 100,'%')" />
						</xsl:attribute>
						<xsl:value-of select="concat (format-number($notVisited div $total * 100,'#.##'),'%')" />
					</td>
				</xsl:if>
				<xsl:if test="not ($visited=0)">
					<td class="visited">
						<xsl:attribute name="width">
							<xsl:value-of select="concat($visited div $total * 100,'%')" />
						</xsl:attribute>
						<xsl:value-of select="concat (format-number($visited div $total * 100,'#.##'), '%')" />
					</td>
				</xsl:if>
			</tr>
		</table>
	</xsl:template>
	<xsl:template name="module">
		<xsl:for-each select="//module">
			<xsl:sort select="@assembly" />
			<xsl:variable name="module" select="./@assembly" />
			<div class="assembly">
				<a class="assembly" name="#{generate-id($module)}">Module 
					<xsl:value-of select="$module" />
				</a>
			</div>
			<xsl:for-each select="./class[@excluded='false']">
				<xsl:sort select="@name" />
				<xsl:call-template name="ClassSummary">
					<xsl:with-param name="module" select="$module" />
					<xsl:with-param name="class" select="./@name" />
				</xsl:call-template>
			</xsl:for-each>
		</xsl:for-each>
		<xsl:variable name="totalMod" select="count(./class/method/seqpnt[@ex='false'])" />
		<xsl:variable name="notvisitedMod" select="count( ./class/method/seqpnt[ @vc='0'][@ex='false'] ) div $totalMod * 100 " />
		<xsl:variable name="visitedMod" select="count(./class/method/seqpnt[not(@vc='0')][@ex='false'] ) div $totalMod * 100" />
	</xsl:template>
	<!-- Class Summary -->
	<xsl:template name="ClassSummary">
		<xsl:param name="module" />
		<xsl:param name="class" />
		<xsl:variable name="total" select="count(//seqpnt[(parent::method/parent::class/@name=$class) and (parent::method/parent::class/parent::module/@assembly=$module) and (@ex='false') ])" />
		<xsl:variable name="notvisited" select="count(//seqpnt[(parent::method/parent::class/@name=$class) and (parent::method/parent::class/parent::module/@assembly=$module) and (@vc='0') and (@ex='false')] )" />
		<xsl:variable name="visited" select="count(//seqpnt[(parent::method/parent::class/@name=$class) and (parent::method/parent::class/parent::module/@assembly=$module) and (not(@vc='0')) and (@ex='false')] )" />
		<xsl:variable name="newid" select="concat (generate-id(), 'class')" />
		<table width='90%' >
			<tr>
				<td width="40%" class="class">
					<xsl:attribute name="onclick">javascript:toggle('<xsl:value-of select="$newid" />')</xsl:attribute>
					<xsl:value-of select="$class" />
				</td>
				<td width="60%">
					<xsl:call-template name="detailPercent">
						<xsl:with-param name="visited" select="$visited" />
						<xsl:with-param name="notVisited" select="$notvisited" />
						<xsl:with-param name="total" select="$total" />
					</xsl:call-template>
				</td>
			</tr>
		</table>
    <table style="display: block;" width="100%" class="collapse">
      <xsl:attribute name="id">
        <xsl:value-of select="$newid" />
      </xsl:attribute>
      <tr>
        <td>
          <xsl:call-template name="Methods">
            <xsl:with-param name="module" select="$module" />
            <xsl:with-param name="class" select="$class" />
          </xsl:call-template>
        </td>
      </tr>
    </table>
    <hr size="1" width='90%' align='left' style=" border-bottom: 1px dotted #999;" />
	</xsl:template>
	<xsl:template name="Methods">
		<xsl:param name="module" />
		<xsl:param name="class" />
		<xsl:for-each select="//method[(parent::class/@name = $class) and (parent::class/parent::module/@assembly=$module)]">
			<xsl:sort select="@name" />
			<xsl:variable name="total" select="count(./seqpnt[@ex='false'])" />
			<xsl:variable name="notvisited" select="count(./seqpnt[@vc='0'][@ex='false'] ) " />
			<xsl:variable name="visited" select="count(./seqpnt[not(@vc='0')][@ex='false'])" />
			<xsl:variable name="methid" select="generate-id(.)" />

      <xsl:if test="@excluded='false'">
			  <table cellpadding="3" cellspacing="0" width="90%">
				  <tr>
					  <td width="45%" class='method'>
						  <xsl:attribute name="onclick">javascript:toggle('<xsl:value-of select="$methid" />')</xsl:attribute>
						  <xsl:value-of select="@name" />
					  </td>
					  <td width="55%">
						  <xsl:call-template name="detailPercent">
							  <xsl:with-param name="visited" select="$visited" />
							  <xsl:with-param name="notVisited" select="$notvisited" />
							  <xsl:with-param name="total" select="$total" />
						  </xsl:call-template>
					  </td>
				  </tr>
			  </table>
			  <xsl:call-template name="seqpnt">
				  <xsl:with-param name="module" select="$module" />
				  <xsl:with-param name="class" select="$class" />
				  <xsl:with-param name="id" select="$methid" />
			  </xsl:call-template>
      </xsl:if>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="seqpnt">
		<xsl:param name="module" />
		<xsl:param name="class" />
		<xsl:param name="id" />
		<table cellpadding="3" cellspacing="0" border='1' bordercolor="black" style="display: block;" class="collapse">
			<xsl:attribute name="id">
				<xsl:value-of select="$id" />
			</xsl:attribute>
			<tr>
				<td class="hdrcell">Visits</td>
				<td class="hdrcell">Line</td>
				<td class="hdrcell">End Line</td>
				<td class="hdrcell">Column</td>
				<td class="hdrcell">End Column</td>
				<td class="hdrcell">Document</td>
			</tr>
			<xsl:for-each select="./seqpnt">
				<xsl:sort select="@l" />
				<xsl:variable name="docme" select="@doc" />
				<tr>
					<td class="datacell">
						<xsl:attribute name="class">
							<xsl:choose>
								<xsl:when test="@ex = 'true'">exdatacell</xsl:when>
								<xsl:when test="@vc = 0">hldatacell</xsl:when>
								<xsl:otherwise>datacell</xsl:otherwise>
							</xsl:choose>
						</xsl:attribute>
						<xsl:choose>
							<xsl:when test="@ex = 'true'">---</xsl:when>
							<xsl:otherwise><xsl:value-of select="@vc" /></xsl:otherwise>
						</xsl:choose>
					</td>
					<td class="datacell">
						<xsl:value-of select="@l" />
					</td>
					<td class="datacell">
						<xsl:value-of select="@el" />
					</td>
					<td class="datacell">
						<xsl:value-of select="@c" />
					</td>
					<td class="datacell">
						<xsl:value-of select="@ec" />
					</td>
					<td class="datacell">
						<xsl:value-of select="//documents/doc[@id=$docme]/@url" />
					</td>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>
	<!--<xsl:template name="ClassSummaryDetail">
		<xsl:param name="module" />
		<xsl:variable name="total" select="count(./method/seqpnt[ @ex='false' ])" />
		<xsl:variable name="notVisited" select="count( ./method/seqpnt[ @vc='0'][ @ex='false' ] )" />
		<xsl:variable name="visited" select="count(./method/seqpnt[not(@vc='0')] )" />
		<td width="35%">
			<div class="assembly">
				<a href="#{generate-id($module)}">
					<xsl:value-of select="$module" />
				</a>
			</div>
		</td>
		<td width="65%">
			<xsl:call-template name="detailPercent">
				<xsl:with-param name="visited" select="$visited" />
				<xsl:with-param name="notVisited" select="$notVisited" />
				<xsl:with-param name="total" select="$total" />
			</xsl:call-template>
		</td>
	</xsl:template>-->
	<!-- General Header -->
	<xsl:template name="header">
    <table>
      <tr>
        <td>
          <h1>
            NCover Code Coverage Report
          </h1>
        </td>
      </tr>
    </table>
    <table>
			<tr>
				<td class="class">
					<a class="class" onClick="ExpandAll();">Expand</a>
				</td>
				<td class="class"> | </td>
				<td class="class">
					<a class="class" onClick="CollapseAll();">Collapse</a>
				</td>
			</tr>
		</table>
		<hr size="1" />
	</xsl:template>
	<xsl:template name="footer">
    <hr size="1" />
    <table>
      <tr>
        <td>
          <a class="detailPercent" href="#top">Top</a>
        </td>
      </tr>
    </table>
	</xsl:template>
</xsl:stylesheet>
