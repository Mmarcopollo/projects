<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
    xmlns:fo="http://www.w3.org/1999/XSL/Format">
    <xsl:output method="xml" encoding="utf-8" />
    <xsl:template match="/">
        <fo:root>
            <fo:layout-master-set>
                <fo:simple-page-master master-name="Generated" page-height="297mm" page-width="210mm" margin-top="20" margin-bottom="40" margin-left="50" margin-right="50">
                    <fo:region-body margin="30,10" />
                    <fo:region-before extent="5" />
                    <fo:region-after extent="5" />
                    <fo:region-start extent="5" />
                    <fo:region-end extent="5" />
                </fo:simple-page-master>
            </fo:layout-master-set>
            <fo:page-sequence master-reference="Generated">
                <fo:static-content flow-name="xsl-region-before">
                    <fo:block text-align="center" font-family="Segoe UI" font-size="20px">
                        <xsl:text>Albums report:</xsl:text>
                        <fo:block text-align="center" font-family="Segoe UI" font-size="10px">
                            [Generated:
                            <xsl:value-of select="substring(//Generated/Report/ReportDate,14,10)" />
                            <xsl:text></xsl:text>
                            <xsl:value-of select="substring(//Generated/Report/ReportDate,25,8)" />
                            ]
                        </fo:block>
                    </fo:block>
                </fo:static-content>
                <fo:static-content flow-name="xsl-region-after">
                    <fo:block text-align="center" font-family="Segoe UI" font-size="10px">
                        <xsl:text>page </xsl:text>
                        <fo:page-number />
                    </fo:block>
                </fo:static-content>
                <fo:flow flow-name="xsl-region-body">
                    <xsl:apply-templates />
                </fo:flow>
            </fo:page-sequence>
        </fo:root>
    </xsl:template>
    <xsl:template match="Author">
        <fo:block font-size="16px" text-align="left" font-family="Segoe UI" margin="25">
            <fo:block>
                <xsl:text>Author:</xsl:text>
                <xsl:text>&#xD;&#xA;</xsl:text>
            </fo:block>
            <fo:block>
                <xsl:apply-templates />
            </fo:block>
        </fo:block>
    </xsl:template>
    <xsl:template match="Author">
        <fo:block font-size="16px" text-align="left" font-family="Segoe UI">
            <fo:block>
                <fo:block margin-left="5mm">
                    <xsl:value-of select="Name" />
                    <xsl:text>&#xD;&#xA;</xsl:text>
                    (
                    <xsl:value-of select="ID" />
                    )
                </fo:block>
            </fo:block>
        </fo:block>
    </xsl:template>
    <xsl:template match="Report">
        <xsl:apply-templates select="Author" />
        <fo:block>
            <fo:table border="solid black" width="100%">
                <fo:table-header>
                    <fo:table-row>
                        <fo:table-cell border="none">
                            <fo:block font-weight="bold" text-align="center">Name</fo:block>
                        </fo:table-cell>
                        <fo:table-cell border="none">
                            <fo:block font-weight="bold" text-align="center">Value</fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                </fo:table-header>
                <fo:table-body>
                    <fo:table-row>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                Number of types of albums
                            </fo:block>
                        </fo:table-cell>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                <xsl:value-of select="./Stats/NumberOfTypesOfAlbums"></xsl:value-of>
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                    <fo:table-row>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                Total price of albums
                            </fo:block>
                        </fo:table-cell>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                <xsl:value-of select="./Stats/sumOdPrice"></xsl:value-of>
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                    <fo:table-row>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                Avarage price of albums
                            </fo:block>
                        </fo:table-cell>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                <xsl:value-of select="./Stats/avaragePrice"></xsl:value-of>
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                    <fo:table-row>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                The most expesive album
                            </fo:block>
                        </fo:table-cell>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                <xsl:value-of select="./Stats/highestPrice"></xsl:value-of>
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                     <fo:table-row>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                The cheapest album
                            </fo:block>
                        </fo:table-cell>
                        <fo:table-cell border="solid black">
                            <fo:block text-align="center">
                                <xsl:value-of select="./Stats/lowestPrice"></xsl:value-of>
                            </fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                </fo:table-body>
            </fo:table>
        </fo:block>
        <fo:block>&#160;</fo:block>
        <fo:block font-weight="bold" font-size="16px" text-align="center">
            Albums
        </fo:block>
        <fo:block>
            <fo:table border="solid black" width="100%">
                <fo:table-header>
                    <fo:table-row>
                        <fo:table-cell border="none">
                            <fo:block font-weight="bold" text-align="center">ID</fo:block>
                        </fo:table-cell>
                        <fo:table-cell border="none">
                            <fo:block font-weight="bold" text-align="center">Type</fo:block>
                        </fo:table-cell>
                    </fo:table-row>
                </fo:table-header>
                <fo:table-body>
                    <xsl:for-each select="./Hip-Hop/OrderId">
                        <fo:table-row>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                    <xsl:value-of select="node()" />
                                </fo:block>
                            </fo:table-cell>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                Hip-Hop
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                    </xsl:for-each>
                    <xsl:for-each select="./POP/OrderId">
                        <fo:table-row>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                    <xsl:value-of select="node()" />
                                </fo:block>
                            </fo:table-cell>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                POP
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                    </xsl:for-each>
                    <xsl:for-each select="./Rock/OrderId">
                        <fo:table-row>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                    <xsl:value-of select="node()" />
                                </fo:block>
                            </fo:table-cell>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                Rock
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                    </xsl:for-each>
                    <xsl:for-each select="./Clasical/OrderId">
                        <fo:table-row>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                    <xsl:value-of select="node()" />
                                </fo:block>
                            </fo:table-cell>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                Clasical
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                    </xsl:for-each>
                    <xsl:for-each select="./Metal/OrderId">
                        <fo:table-row>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                    <xsl:value-of select="node()" />
                                </fo:block>
                            </fo:table-cell>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                Metal
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                    </xsl:for-each>
                    <xsl:for-each select="./Disco/OrderId">
                        <fo:table-row>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                    <xsl:value-of select="node()" />
                                </fo:block>
                            </fo:table-cell>
                            <fo:table-cell border="solid black">
                                <fo:block text-align="center">
                                Disco
                                </fo:block>
                            </fo:table-cell>
                        </fo:table-row>
                    </xsl:for-each>
                </fo:table-body>
            </fo:table>
        </fo:block>
    </xsl:template>
</xsl:stylesheet>