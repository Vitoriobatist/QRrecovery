> Recupera QR Codes ilegíveis por meio de um pipeline de Processamento Digital de Imagens (PDI), com interface em C# e motor de processamento em Python/OpenCV.

---

## Sobre o Projeto

QR Codes podem se tornar ilegíveis por ruído digital, baixa qualidade de impressão ou embaralhamento geométrico proposital. O **QR Recovery** aplica uma sequência de filtros de PDI para restaurar esses códigos e torná-los escaneáveis novamente.

Desenvolvido como projeto acadêmico de aplicação prática de técnicas de visão computacional.

---

## Tecnologias

| Camada | Tecnologia |
|--------|-----------|
| Interface | C# — Windows Forms (.NET) |
| Processamento | Python 3 — OpenCV, NumPy |
| Comunicação | Arquivos PNG temporários |

---

## Pipeline de Processamento

```
Imagem degradada
      ↓
1. Desembaralhar       → Reverte embaralhamento por faixas (seed fixa)
      ↓
2. Escala de cinza     → Reduz 3 canais RGB para 1 canal de intensidade
      ↓
3. Filtro Mediana      → Remove ruído sal e pimenta (kernel 3×3)
      ↓
4. Filtro Gaussiano    → Suaviza ruído contínuo (kernel 3×3, sigma auto)
      ↓
5. Threshold de Otsu   → Binarização automática (preto e branco puro)
      ↓
6. Dilatação / Erosão  → Operações morfológicas para restaurar módulos
      ↓
QR Code restaurado ✅
```

---

## Funcionalidades

- **Desembaralho geométrico** — reconstrói faixas verticais embaralhadas com semente conhecida (`seed=1234`)
- **Suavização adaptável** — escolha entre filtro Mediana ou Gaussiano conforme o tipo de ruído
- **Binarização automática** — o método de Otsu calcula o limiar ideal sem intervenção manual
- **Morfologia ajustável** — dilatação e erosão independentes para cada tipo de degradação
- **AbsDiff visual** — exibe a diferença entre etapas para auditar o impacto de cada filtro
- **Histórico de etapas** — navegue entre os estados intermediários do processamento pela interface

---

## Como Executar

### Pré-requisitos

```bash
# Python
pip install opencv-python numpy

# .NET (para rodar a interface C#)
# Visual Studio 2019+ ou .NET 6+
```

### Rodando

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/qr-recovery.git
   cd qr-recovery
   ```

2. Abra a solução `.sln` no Visual Studio e execute o projeto C#.

3. Na interface, carregue uma imagem de QR Code degradado e aplique os filtros desejados em sequência.

> O C# chama automaticamente o script `processar.py` passando a imagem via arquivo temporário.

---

## Estrutura do Projeto

```
qr-recovery/
├── QRRecovery.sln          # Solução Visual Studio
├── QRRecovery/             # Projeto C# (Windows Forms)
│   ├── Form1.cs            # Interface principal
│   └── ...
├── scripts/
│   └── processar.py        # Backend Python com todos os filtros
└── README.md
```

---

## Filtros Disponíveis

| Filtro | Parâmetro (`processar.py`) | Descrição |
|--------|---------------------------|-----------|
| Desembaralhar | `desembaralhar` | Reconstrói faixas embaralhadas |
| Escala de cinza | `cinza` | Converte para 1 canal |
| Mediana | `mediana` | Remove ruído sal e pimenta |
| Gaussiano | `gaussiano` | Suavização contínua |
| Otsu | `otsu` | Binarização automática |
| Dilatação | `dilatar` | Expande regiões brancas |
| Erosão | `erodir` | Encolhe regiões brancas |
| AbsDiff | `absdiff` | Visualiza diferença entre etapas |

---
