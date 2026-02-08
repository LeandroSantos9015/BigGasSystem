## QuestPDF — padrão

Separar:
- ReportModel (dados prontos)
- ReportDocument (layout QuestPDF)
- ReportDataProvider (consulta e monta model)

Regras:
- Document não acessa banco.
- Controller coordena e valida permissão.
- View só aciona.
