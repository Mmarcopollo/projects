<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:svg="http://www.w3.org/2000/svg" xmlns="http://www.w3.org/2000/svg" version="1.0">
    <xsl:output method="xml" media-type="image/svg" encoding="utf-8" doctype-public="-//W3C//DTD SVG 1.1//EN" doctype-system="http://www.w3.org/TR/2001/REC-SVG-20010904/DTD/svg10.dtd" />
    <xsl:template match="/">
        <svg:svg width="800" height="600" font-family="Calibri">
            <svg:title>
                Types of albums report
            </svg:title>

            <rect x="100" y="30" width="400" height="320" fill="grey" stroke="black" />


            <rect x="110" y="35" width="{count(/Generated/Report/Hip-Hop/OrderId)*40}" height="30" fill="#blue">
                <animate attributeName="width"  from="0" to="{count(/Generated/Report/Hip-Hop/OrderId)*40}" dur="5s" fill="freeze" />
            </rect>
            <svg:text x="490" y="55" fill="black" font-weight="bold" text-anchor="middle">
                <xsl:value-of select="count(/Generated/Report/Hip-Hop/OrderId)" />
            </svg:text>

            <rect x="110" y="75" width="{count(/Generated/Report/POP/OrderId)*40}" height="30" fill="#DF0101">
                <animate attributeName="width"  from="0" to="{count(/Generated/Report/POP/OrderId)*40}" dur="5s" fill="freeze" />
            </rect>
            <svg:text x="490" y="95" fill="black" font-weight="bold" text-anchor="middle">
                <xsl:value-of select="count(/Generated/Report/POP/OrderId)" />
            </svg:text>

            <rect x="110" y="115" width="{count(/Generated/Report/Rock/OrderId)*40}" height="30" fill="#000000">
                <animate attributeName="width" from="0" to="{count(/Generated/Report/Rock/OrderId)*40}" dur="5s" fill="freeze" />
            </rect>
            <svg:text x="490" y="135" fill="black" font-weight="bold" text-anchor="middle">
                <xsl:value-of select="count(/Generated/Report/Rock/OrderId)" />
            </svg:text>

            <rect x="110" y="155" width="{count(/Generated/Report/Clasical/OrderId)*40}" height="30" fill="#01DF01">
                <animate attributeName="width" from="0" to="{count(/Generated/Report/Clasical/OrderId)*40}" dur="5s" fill="freeze" />
            </rect>
            <svg:text x="490" y="175" fill="black" font-weight="bold" text-anchor="middle">
                <xsl:value-of select="count(/Generated/Report/Clasical/OrderId)" />
            </svg:text>

            <rect x="110" y="195" width="{count(/Generated/Report/Metal/OrderId)*40}" height="30" fill="#FF00BF">
                <animate attributeName="width" from="0" to="{count(/Generated/Report/Metal/OrderId)*40}" dur="5s" fill="freeze" />
            </rect>
            <svg:text x="490" y="215" fill="black" font-weight="bold" text-anchor="middle">
                <xsl:value-of select="count(/Generated/Report/Metal/OrderId)" />
            </svg:text>

			<rect x="110" y="235" width="{count(/Generated/Report/Disco/OrderId)*40}" height="30" fill="#FFFFFF">
                <animate attributeName="width" from="0" to="{count(/Generated/Report/Disco/OrderId)*40}" dur="5s" fill="freeze" />
            </rect>
            <svg:text x="490" y="255" fill="black" font-weight="bold" text-anchor="middle">
                <xsl:value-of select="count(/Generated/Report/Disco/OrderId)" />
            </svg:text>


            <svg:text x="300" y="0" font-size="18" fill="black" font-weight="bold" text-anchor="middle">
                Types od albums report
                <animate attributeName="y" dur="5s" fill="freeze" from="0" to="50" />
            </svg:text>
            <line id="axis-y" x1="105" y1="30" x2="105" y2="270" style="fill:none;stroke:rgb(0,0,0);stroke-width:2" />
            <line id="axis-x" x1="105" y1="270" x2="490" y2="270" style="fill:none;stroke:rgb(0,0,0);stroke-width:2" />
            <svg:text x="300" y="290" font-size="18" fill="black" font-weight="bold" text-anchor="middle">
                Legend
            </svg:text>
            <rect x="120" y="300" width="40" height="20" fill="blue" stroke="black" />
            <svg:text x="140" y="330" fill="black" font-weight="bold" font-size="12" text-anchor="middle">
                Hip-Hop
            </svg:text>
            <rect x="184" y="300" width="40" height="20" fill="#DF0101" stroke="black" />
            <svg:text x="204" y="330" fill="black" font-weight="bold" font-size="12" text-anchor="middle">
                Pop
            </svg:text>
            <rect x="248" y="300" width="40" height="20" fill="#000000" stroke="black" />
            <svg:text x="268" y="330" fill="black" font-weight="bold" font-size="12" text-anchor="middle">
                Rock
            </svg:text>
            <rect x="312" y="300" width="40" height="20" fill="#01DF01" stroke="black" />
            <svg:text x="332" y="330" fill="black" font-weight="bold" font-size="12" text-anchor="middle">
                Clasical
            </svg:text>
            <rect x="376" y="300" width="40" height="20" fill="#FF00BF" stroke="black" />
            <svg:text x="397" y="330" fill="black" font-weight="bold" font-size="12" text-anchor="middle">
                Metal
            </svg:text>
			<rect x="440" y="300" width="40" height="20" fill="#FFFFFF" stroke="black" />
            <svg:text x="460" y="330" fill="black" font-weight="bold" font-size="12" text-anchor="middle">
                Disco
            </svg:text>
            <svg:g class="btn" onclick="alert('Author:\nMarek Szafran 210329')" cursor="pointer">
                <svg:rect x="270" y="360" width="60" height="20" fill="blue" stroke="black" />
                <svg:text x="274" y="375" fill="white" font-weight="bold" font-size="16">Author</svg:text>
            </svg:g>
            
        </svg:svg>
    </xsl:template>
    
    
</xsl:stylesheet>