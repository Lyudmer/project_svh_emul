<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml"/>
	<xsl:param name="docid"/>
	<xsl:param name="Now"/>
	<xsl:template match="*|/">
		<xsl:apply-templates select="//*[local-name()='WHDocInventory'][position()=last()]"/>
	</xsl:template>
	<xsl:template match="*[local-name()='WHDocInventory'][position()=last()]">
		<ConfirmWHDocReg DocumentModeID="1008015E">
			<DocumentID><xsl:value-of select="$docid"/></DocumentID>
			<RegDate><xsl:value-of select="substring($Now,1,10)"/></RegDate>
			<PresentDate><xsl:value-of select="substring($Now,1,10)"/></PresentDate>
			<xsl:for-each select="*[local-name()='Receiver']/*[local-name()='Customs']">
				<Customs>
					<Code><xsl:value-of select="*[local-name()='Code']"/></Code>
					<OfficeName><xsl:value-of select="*[local-name()='OfficeName']"/></OfficeName>
				</Customs>
			</xsl:for-each>
			<xsl:for-each select="*[local-name()='Participant']">
				<Organization>
					<OrganizationName><xsl:value-of select="*[local-name()='OrganizationName']"/></OrganizationName>
				</Organization>
			</xsl:for-each>	
			<CustomsPerson>
				<PersonSurname>Фамилия</PersonSurname>
				<PersonName>Имя</PersonName>
				<PersonMiddleName>Отчество</PersonMiddleName>
				<PersonPost>Должность</PersonPost>
				<LNP>000</LNP>
			</CustomsPerson>
			<RegNumberDoc>
				<CustomsCode>11111111</CustomsCode>
				<RegistrationDate><xsl:value-of select="substring($Now,1,10)"/></RegistrationDate>
				<GTDNumber>0000001</GTDNumber>
			</RegNumberDoc>
			<WarehouseLicense>
				<CertificateKind>lic_Permition</CertificateKind>
				<CertificateNumber>11111/111111/00001/1</CertificateNumber>
				<CertificateDate>2017-10-26</CertificateDate>
			</WarehouseLicense>
		</ConfirmWHDocReg>
	</xsl:template>
</xsl:stylesheet>
