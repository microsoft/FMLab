<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="text" />
  <xsl:template match="FleetCustomer">
    FirstName:<xsl:value-of select="@FirstName"/> LastName <xsl:value-of select="@LastName"/>
  </xsl:template>
</xsl:stylesheet>