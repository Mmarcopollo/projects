<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
    xmlns="http://www.w3.org/1999/xhtml">
    <xsl:output method="xml" version="1.0" encoding="utf-8" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd" />
    <xsl:template match="/">
        <xsl:element name="html">
            <xsl:copy-of select="document('')/xsl:stylesheet/namespace::*[not(local-name() = 'xsl')]" />
            <xsl:attribute name="xml:lang">pl</xsl:attribute>
            <xsl:attribute name="lang">pl</xsl:attribute>

            <xsl:element name="head">
                <xsl:element name="link">
                    <xsl:attribute name="rel">
                        <xsl:text>stylesheet</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="type">
                        <xsl:text>text/css</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="href">
                        <xsl:text> https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css</xsl:text>
                    </xsl:attribute>
                </xsl:element>
                <xsl:element name="meta">
                    <xsl:attribute name="name">
                        <xsl:text>description</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="content">
                        <xsl:text>Report</xsl:text>
                    </xsl:attribute>
                </xsl:element>
                <xsl:element name="meta">
                    <xsl:attribute name="name">
                        <xsl:text>author</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="content">
                        <xsl:text>Marek Szafran</xsl:text>
                    </xsl:attribute>
                </xsl:element>
                <xsl:element name="meta">
                    <xsl:attribute name="http-equiv">
                        <xsl:text>content-type</xsl:text>
                    </xsl:attribute>
                    <xsl:attribute name="content">
                        <xsl:text>text/html;charset=UTF-8</xsl:text>
                    </xsl:attribute>
                </xsl:element>
                <xsl:element name="title">
                    <xsl:text>Report XHTML</xsl:text>
                </xsl:element>
            </xsl:element>

            <xsl:element name="body">
                <div class="container mt-3">
                    <xsl:element name="div">
                        <xsl:attribute name="class">
                            <xsl:text>links</xsl:text>
                        </xsl:attribute>
                        <xsl:element name="a">
                            <xsl:attribute name="href">
                                <xsl:text>#authors</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="class">
                                <xsl:text>links</xsl:text>
                            </xsl:attribute>
                            <xsl:text>Author</xsl:text>
                        </xsl:element>
                        <xsl:text>&#x9;|&#x9;</xsl:text>
                        <xsl:element name="a">
                            <xsl:attribute name="href">
                                <xsl:text>#report</xsl:text>
                            </xsl:attribute>
                            <xsl:attribute name="class">
                                <xsl:text>links</xsl:text>
                            </xsl:attribute>
                            <xsl:text>Report</xsl:text>
                        </xsl:element>
                    </xsl:element>
                    <xsl:apply-templates />
                </div>
            </xsl:element>
        </xsl:element>
    </xsl:template>

    <xsl:template match="info">
        <xsl:element name="div">
            <xsl:attribute name="class">
                <xsl:text>authors</xsl:text>
            </xsl:attribute>
            <xsl:element name="p">
                <xsl:attribute name="class">
                    <xsl:text>authors-title</xsl:text>
                </xsl:attribute>
                    <xsl:element name="a">
                        <xsl:attribute name="name">
                            <xsl:text>authors</xsl:text>
                        </xsl:attribute>
                        <xsl:text>Author:&#x20;</xsl:text>
                    </xsl:element>
            </xsl:element>
            <xsl:apply-templates />
        </xsl:element>
    </xsl:template>

    <xsl:template match="creator">
        <xsl:element name="p">
            <xsl:attribute name="class">
                <xsl:text>author</xsl:text>
            </xsl:attribute>
            <xsl:value-of select="." />
            <xsl:text>&#x20;(</xsl:text>
            <xsl:value-of select="./@index" />
            <xsl:text>)</xsl:text>
        </xsl:element>
    </xsl:template>

    <xsl:template match="types">
        <xsl:element name="div">
            <xsl:attribute name="class">
                <xsl:text>report</xsl:text>
            </xsl:attribute>
            <xsl:element name="p">
                <xsl:attribute name="class">
                    <xsl:text>report-title</xsl:text>
                </xsl:attribute>
                    <xsl:element name="a">
                        <xsl:attribute name="name">
                            <xsl:text>report</xsl:text>
                        </xsl:attribute>
                        <xsl:text>Report:</xsl:text>
                    </xsl:element>
            </xsl:element>
        </xsl:element>
        <xsl:element name="p">
            <xsl:attribute name="class">
                <xsl:text>types-of-products</xsl:text>
            </xsl:attribute>
            <xsl:text>Number of types od music: </xsl:text>
            <xsl:value-of select="count(/albumCollection/types/type)" />
        </xsl:element>
    </xsl:template>

    <xsl:template match="albums">
        <xsl:element name="p">
            <xsl:attribute name="class">
                <xsl:text>sold-products</xsl:text>
            </xsl:attribute>
            <xsl:text>Value of albums: </xsl:text>
            <xsl:value-of select="concat(sum(/albumCollection/albums/album/price), 'zł')" />
        </xsl:element>
        <xsl:element name="p">
            <xsl:attribute name="class">
                <xsl:text>avarage-value</xsl:text>
            </xsl:attribute>
            <xsl:text>Avarage value of albums: </xsl:text>
            <xsl:value-of select="concat(round((sum(//albumCollection/albums/album/price) div count(//albumCollection/albums/album))*100)div 100, 'zł')" />
        </xsl:element>

        <xsl:element name="p">
            <xsl:attribute name="class">
                <xsl:text>highest-price</xsl:text>
            </xsl:attribute>
            <xsl:text>Most expensive album: </xsl:text>
            <xsl:variable name="highestPrice">
                <xsl:for-each select="(/albumCollection/albums/album/price)">
                    <xsl:sort data-type="number" order="descending" />
                    <xsl:if test="position()=1">
                        <xsl:value-of select="." />
                    </xsl:if>
                </xsl:for-each>
            </xsl:variable>
            <xsl:value-of select="concat(format-number($highestPrice,'0.00'), 'zł')" />
        </xsl:element>


        <xsl:element name="p">
            <xsl:attribute name="class">
                <xsl:text>Cheapest-price</xsl:text>
            </xsl:attribute>
            <xsl:text>Cheapest album: </xsl:text>
            <xsl:variable name="lowestPrice">
                <xsl:for-each select="(/albumCollection/albums/album/price)">
                    <xsl:sort data-type="number" order="ascending" />
                    <xsl:if test="position()=1">
                        <xsl:value-of select="." />
                    </xsl:if>
                </xsl:for-each>
            </xsl:variable>
            <xsl:value-of select="concat(format-number($lowestPrice,'0.00'), 'zł')" />
        </xsl:element>



        <xsl:element name="div">
            <xsl:attribute name="class">
                <xsl:text>orders</xsl:text>
            </xsl:attribute>
            <xsl:element name="h4">
                <xsl:text>Types:</xsl:text>
            </xsl:element>
            <xsl:element name="div">
                <xsl:attribute name="class">
                    <xsl:text>new</xsl:text>
                </xsl:attribute>
                <h4 class="text-center">Hip-Hop</h4>
                <table class="table table-dark table-striped">
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                    </tr>
                    <xsl:for-each select="/albumCollection/albums/album[@typeId = 'G1']">
                        <tr>
                            <td>
                                <xsl:value-of select="title" />
                            </td>
                            <td>
                                <xsl:value-of select="author" />
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </xsl:element>
        </xsl:element>
        <xsl:element name="div">
            <xsl:attribute name="class">
                <xsl:text>orders</xsl:text>
            </xsl:attribute>
            <xsl:element name="div">
                <xsl:attribute name="class">
                    <xsl:text>new</xsl:text>
                </xsl:attribute>
                <h4 class="text-center">POP</h4>
                <table class="table table-dark table-striped">
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                    </tr>
                    <xsl:for-each select="/albumCollection/albums/album[@typeId = 'G2']">
                        <tr>
                            <td>
                                <xsl:value-of select="title" />
                            </td>
                            <td>
                                <xsl:value-of select="author" />
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </xsl:element>
        </xsl:element>

        <xsl:element name="div">
            <xsl:attribute name="class">
                <xsl:text>orders</xsl:text>
            </xsl:attribute>
            <xsl:element name="div">
                <xsl:attribute name="class">
                    <xsl:text>new</xsl:text>
                </xsl:attribute>
                <h4 class="text-center">ROCK</h4>
                <table class="table table-dark table-striped">
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                    </tr>
                    <xsl:for-each select="/albumCollection/albums/album[@typeId = 'G3']">
                        <tr>
                            <td>
                                <xsl:value-of select="title" />
                            </td>
                            <td>
                                <xsl:value-of select="author" />
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </xsl:element>
        </xsl:element>

        <xsl:element name="div">
            <xsl:attribute name="class">
                <xsl:text>orders</xsl:text>
            </xsl:attribute>
            <xsl:element name="div">
                <xsl:attribute name="class">
                    <xsl:text>new</xsl:text>
                </xsl:attribute>
                <h4 class="text-center">CLASICAL MUSIC</h4>
                <table class="table table-dark table-striped">
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                    </tr>
                    <xsl:for-each select="/albumCollection/albums/album[@typeId = 'G4']">
                        <tr>
                            <td>
                                <xsl:value-of select="title" />
                            </td>
                            <td>
                                <xsl:value-of select="author" />
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </xsl:element>
        </xsl:element>

        <xsl:element name="div">
            <xsl:attribute name="class">
                <xsl:text>orders</xsl:text>
            </xsl:attribute>
            <xsl:element name="div">
                <xsl:attribute name="class">
                    <xsl:text>new</xsl:text>
                </xsl:attribute>
                <h4 class="text-center">METAL</h4>
                <table class="table table-dark table-striped">
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                    </tr>
                    <xsl:for-each select="/albumCollection/albums/album[@typeId = 'G5']">
                        <tr>
                            <td>
                                <xsl:value-of select="title" />
                            </td>
                            <td>
                                <xsl:value-of select="author" />
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </xsl:element>
        </xsl:element>

        <xsl:element name="div">
            <xsl:attribute name="class">
                <xsl:text>orders</xsl:text>
            </xsl:attribute>
            <xsl:element name="div">
                <xsl:attribute name="class">
                    <xsl:text>new</xsl:text>
                </xsl:attribute>
                <h4 class="text-center">DISCO POLO</h4>
                <table class="table table-dark table-striped">
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                    </tr>
                    <xsl:for-each select="/albumCollection/albums/album[@typeId = 'G6']">
                        <tr>
                            <td>
                                <xsl:value-of select="title" />
                            </td>
                            <td>
                                <xsl:value-of select="author" />
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
            </xsl:element>
        </xsl:element>
    </xsl:template>
</xsl:stylesheet>