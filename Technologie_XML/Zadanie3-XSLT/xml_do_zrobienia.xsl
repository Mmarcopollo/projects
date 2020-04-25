<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
    xmlns:date="http://exslt.org/dates-and-times" extension-element-prefixes="date">
    <xsl:output method="xml" version="1.0" encoding="UTF-8" media-type="text/xml" omit-xml-declaration="no" indent="yes"></xsl:output>
    <xsl:key use="@typeId" name="id" match="//types/type" />
    <xsl:template match="node()|@*">
        <xsl:copy>
            <xsl:apply-templates select="*[not(node())]" />
        </xsl:copy>
    </xsl:template>
    <xsl:template match="/">
        <xsl:element name="Generated">
            <xsl:apply-templates />
            <Report>
                <Stats>
                    <NumberOfTypesOfAlbums>
                        <xsl:value-of select="count(//albumCollection/albums/album)" />
                    </NumberOfTypesOfAlbums>
                    <sumOdPrice>
                        <xsl:value-of select="concat(sum(//albumCollection/albums/album/price), 'zł')" />
                    </sumOdPrice>
                    <avaragePrice>
                        <xsl:value-of select="concat(round((sum(//albumCollection/albums/album/price) div count(//albumCollection/albums/album))*100)div 100, 'zł')"/>
                    </avaragePrice>
                    <highestPrice>
                        <xsl:variable name="highestPrice">
                            <xsl:for-each select="(/albumCollection/albums/album/price)">
                                <xsl:sort data-type="number" order="descending" />
                                <xsl:if test="position()=1">
                                    <xsl:value-of select="." />
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:value-of select="concat(format-number($highestPrice,'0.00'), 'zł')" />
                    </highestPrice>
                    <lowestPrice>
                        <xsl:variable name="lowestPrice">
                            <xsl:for-each select="(/albumCollection/albums/album/price)">
                                <xsl:sort data-type="number" order="ascending" />
                                <xsl:if test="position()=1">
                                    <xsl:value-of select="." />
                                </xsl:if>
                            </xsl:for-each>
                        </xsl:variable>
                        <xsl:value-of select="concat(format-number($lowestPrice,'0.00'), 'zł')" />
                    </lowestPrice>
                </Stats>

                <Hip-Hop>
                    <xsl:for-each select="//albumCollection/albums/album[@typeId = 'G1']">
                        <OrderId>
                            <xsl:value-of select="title" />
                        </OrderId>
                    </xsl:for-each>
                </Hip-Hop>

                <POP>
                    <xsl:for-each select="//albumCollection/albums/album[@typeId = 'G2']">
                        <OrderId>
                            <xsl:value-of select="title" />
                        </OrderId>
                    </xsl:for-each>
                </POP>

                <Rock>
                    <xsl:for-each select="//albumCollection/albums/album[@typeId = 'G3']">
                        <OrderId>
                            <xsl:value-of select="title" />
                        </OrderId>
                    </xsl:for-each>
                </Rock>

                <Clasical>
                    <xsl:for-each select="//albumCollection/albums/album[@typeId = 'G4']">
                        <OrderId>
                            <xsl:value-of select="title" />
                        </OrderId>
                    </xsl:for-each>
                </Clasical>

                <Metal>
                    <xsl:for-each select="//albumCollection/albums/album[@typeId = 'G5']">
                        <OrderId>
                            <xsl:value-of select="title" />
                        </OrderId>
                    </xsl:for-each>
                </Metal>

                <Disco>
                    <xsl:for-each select="//albumCollection/albums/album[@typeId = 'G6']">
                        <OrderId>
                            <xsl:value-of select="title" />
                        </OrderId>
                    </xsl:for-each>
                </Disco>

                <Author>
                    <Name>
                        <xsl:value-of select="//albumCollection/info/creator" />
                    </Name>
                    <ID>
                        <xsl:value-of select="//albumCollection/info/creator/@index" />
                    </ID>
                </Author>
                <ReportDate>
                    <Date>
                        <xsl:value-of select="date:date-time()" />
                    </Date>
                </ReportDate>
            </Report>
        </xsl:element>
    </xsl:template>
</xsl:stylesheet>