<?xml version="1.0" encoding="UTF-8" ?>
<!-- was: no XML declaration present -->
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text" indent="yes" />
    <xsl:template match="Report">
        <xsl:text>&#xa;</xsl:text>
        <xsl:text>REPORT: </xsl:text>
        <xsl:text>&#xa;</xsl:text>
        <xsl:text>&#xa;</xsl:text>

        <xsl:value-of select="concat(substring('AUTHOR 	                      ', 1, 30), '|   ')" />
        <xsl:value-of select="./Author/Name" />
        <xsl:text> (</xsl:text>
        <xsl:value-of select="./Author/ID" />
        <xsl:text>)&#xa;</xsl:text>

        <xsl:value-of select="concat(substring('GENERATED 	                   ', 1, 30), '|   ')" />
        <xsl:value-of select="substring(./ReportDate/Date,0,11)" />
        <xsl:text>&#xa;</xsl:text>

        <xsl:value-of select="concat(substring('NUMBER OF OF TYPES OF ALBUMS  ', 1, 30), '|   ')" />
        <xsl:value-of select="./Stats/NumberOfTypesOfAlbums" />
        <xsl:text>&#xa;</xsl:text>

        <xsl:value-of select="concat(substring('TOTAL PRICE OF ALBUMS         ', 1, 30), '|   ')" />
        <xsl:value-of select="./Stats/sumOdPrice" />
        <xsl:text>&#xa;</xsl:text>

        <xsl:value-of select="concat(substring('AVARAGE PRICE OF ALBUMS         ', 1, 30), '|   ')" />
        <xsl:value-of select="./Stats/avaragePrice" />
        <xsl:text>&#xa;</xsl:text>

        <xsl:value-of select="concat(substring('HIGHEST PRICE OF ALBUMS         ', 1, 30), '|   ')" />
        <xsl:value-of select="./Stats/highestPrice" />
        <xsl:text>&#xa;</xsl:text>

        <xsl:value-of select="concat(substring('LOWEST PRICE OF ALBUMS         ', 1, 30), '|   ')" />
        <xsl:value-of select="./Stats/lowestPrice" />
        <xsl:text>&#xa;</xsl:text>

        <xsl:text>&#xa;</xsl:text>
        <xsl:text>ALBUMS: </xsl:text>
        <xsl:text>&#xa;</xsl:text>
        <xsl:text>&#xa;</xsl:text>

        <xsl:for-each select="./Hip-Hop/OrderId">
            <xsl:value-of select="concat(substring(concat(.,'                                                           '),0,60),'|     ')" />
            <xsl:text>HIP-HOP</xsl:text>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>

        <xsl:for-each select="./POP/OrderId">
            <xsl:value-of select="concat(substring(concat(.,'                                                               '),0,60),'|     ')" />
            <xsl:text>POP</xsl:text>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>

        <xsl:for-each select="./Rock/OrderId">
            <xsl:value-of select="concat(substring(concat(.,'                                                                 '),0,60),'|     ')" />
            <xsl:text>ROCK</xsl:text>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>

        <xsl:for-each select="./Clasical/OrderId">
            <xsl:value-of select="concat(substring(concat(.,'                                                                     '),0,60),'|     ')" />
            <xsl:text>CLASICAL</xsl:text>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>

        <xsl:for-each select="./Metal/OrderId">
            <xsl:value-of select="concat(substring(concat(.,'                                                                    '),0,60),'|     ')" />
            <xsl:text>METAL</xsl:text>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>

        <xsl:for-each select="./Disco/OrderId">
            <xsl:value-of select="concat(substring(concat(.,'                                                                '),0,60),'|     ')" />
            <xsl:text>DISCO POLO</xsl:text>
            <xsl:text>&#xa;</xsl:text>
        </xsl:for-each>




    </xsl:template>
</xsl:stylesheet>