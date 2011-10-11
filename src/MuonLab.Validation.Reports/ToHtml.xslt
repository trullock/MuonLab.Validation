<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="xml" indent="yes"/>
	<xsl:template match="assembly">
		<html>
			<head>
				<title>Validation report for <xsl:value-of select="@Name"/></title>
				<style type="text/css">
					*
					{
					margin: 0;
					padding: 0;
					}

					html, body
					{
					height: 100%;
					}

					html, body, form, fieldset, input, select, textarea
					{
					font-size: 100.01%;
					}

					h1, h2, h3, h4, h5, h6,
					p,
					pre,
					blockquote,
					ul,
					ol,
					dl,
					address,
					.clear
					{
					font-weight: normal;
					font-style: normal;
					clear:both;
					}

					li
					{
					list-style: none;
					}

					fieldset,
					img
					{
					border: none;
					}

					input,
					select,
					textarea
					{
					outline: none;
					}

					body
					{
					/* This sets the font size to 10px equivalent */
					font-size: 0.625em;
					font-family: Georgia, Garamond, Times New Roman, Serif;
					color: #333;
					padding: 2em;
					}

					h1
					{
					font-size: 3em;
					font-weight: bold;
					font-style: italic;
					margin-bottom: 1em;
					color: #B8112A;
					}
					h2
					{
					font-size: 2.6em;
					font-weight: bold;
					font-style: italic;
					color: #FF5E19;
					}
					h3
					{
					font-size: 2em;
					font-weight: bold;
					color: #F21133;
					}
					h4
					{
					font-size: 1.6em;
					font-weight: bold;
					color: #333;
					margin-top: 1em;
					}
					h5
					{
					font-size: 1.4em;
					font-weight: bold;
					color: #555;
					margin-bottom: 0.5em;
					}
					h5 span
					{
					color: #3060C0;
					padding-right: 2em;
					}
					p
					{
					font-size: 1.4em;
					margin-bottom: 1em;
					}
					p.fullName
					{
					font-size: 1.2em;
					margin-bottom: 2em;
					color: #666;
					}
					ul
					{
					padding-left: 3em;
					margin-bottom: 1em;
					}

					ul li
					{

					}
				</style>
			</head>
			<body>
				<h1>Validation Report for <xsl:value-of select="@Name"/></h1>
				<p>Validated Classes:</p>
				<ul>
					<xsl:for-each select="for">
						<li>
							<h2>
								<xsl:value-of select="@Name"/>
							</h2>
							<p class="fullName">
								<xsl:value-of select="@AssemblyQualifiedName"/>
							</p>
							<ul>
							<xsl:for-each select="validator">
								<li>
								<h3>
									<xsl:value-of select="@Name"/>
								</h3>
									<p class="fullName">
										<xsl:value-of select="@AssemblyQualifiedName"/>
									</p>
									<xsl:choose>
										<xsl:when test="0 &lt; count(properties/property)">
											<ul>
												<xsl:for-each select="properties/property">
													<li>
														<h4>
															<xsl:value-of select="name"/>
														</h4>
														<ul>
															<xsl:for-each select="rule">
																<li>
																	<h5>
																		<xsl:value-of select="condition"/>
																		<xsl:if test="0 &lt; count(arguments/argument)">
																			:
																			<xsl:for-each select="arguments/argument">
																				<span>
																					<xsl:value-of select="."/>
																				</span>
																			</xsl:for-each>
																		</xsl:if>
																	</h5>
																</li>
															</xsl:for-each>
														</ul>
													</li>
												</xsl:for-each>
											</ul>
										</xsl:when>
										<xsl:otherwise>
											<p>There are no rules for this validator.</p>
										</xsl:otherwise>
									</xsl:choose>
								</li>
							</xsl:for-each>
							</ul>
						</li>
					</xsl:for-each>
				</ul>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>