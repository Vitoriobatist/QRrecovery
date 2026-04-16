import cv2
import numpy as np
import sys
import os

def desembaralhar(img, strip_w=8, seed=1234):
    h, w = img.shape[:2]
    num_strips = w // strip_w

    perm = np.arange(num_strips)
    rng = np.random.default_rng(seed)
    rng.shuffle(perm)

    map_x = np.zeros((h, w), dtype=np.float32)
    map_y = np.zeros((h, w), dtype=np.float32)

    for dst_strip in range(num_strips):
        src_strip = perm[dst_strip]
        for x in range(strip_w):
            map_x[:, src_strip * strip_w + x] = dst_strip * strip_w + x
            map_y[:, src_strip * strip_w + x] = np.arange(h)

    return cv2.remap(img, map_x, map_y, interpolation=cv2.INTER_NEAREST)

def to_gray(img):
    if len(img.shape) == 3:
        return cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    return img

def mediana(img):
    gray = to_gray(img)
    return cv2.medianBlur(gray, 3)

def gaussiano(img):
    gray = to_gray(img)
    return cv2.GaussianBlur(gray, (3, 3), 0)

def threshold_preto(img):
    gray = to_gray(img)
    blur = cv2.GaussianBlur(gray, (3, 3), 0)
    _, out = cv2.threshold(blur, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
    return out

def dilatacao(img):
    gray = to_gray(img)
    kernel = np.ones((3, 3), np.uint8)
    return cv2.dilate(gray, kernel, iterations=1)

def erosao(img):
    gray = to_gray(img)
    kernel = np.ones((3, 3), np.uint8)
    return cv2.erode(gray, kernel, iterations=1)

def absdiff_img(img1, img2):
    g1 = to_gray(img1)
    g2 = to_gray(img2)
    return cv2.absdiff(g1, g2)

def main():
    if len(sys.argv) < 4:
        print("Uso incorreto")
        sys.exit(1)

    operacao = sys.argv[1]
    entrada = sys.argv[2]
    saida = sys.argv[3]

    if not os.path.exists(entrada):
        print("Entrada não encontrada")
        sys.exit(1)

    img = cv2.imread(entrada, cv2.IMREAD_COLOR)
    if img is None:
        print("Erro ao abrir imagem")
        sys.exit(1)

    if operacao == "desembaralhar":
        out = desembaralhar(img)

    elif operacao == "cinza":
        out = to_gray(img)

    elif operacao == "mediana":
        out = mediana(img)

    elif operacao == "gaussiano":
        out = gaussiano(img)

    elif operacao == "threshold":
        out = threshold_preto(img)

    elif operacao == "dilatacao":
        out = dilatacao(img)

    elif operacao == "erosao":
        out = erosao(img)

    elif operacao == "absdiff":
        if len(sys.argv) < 5:
            print("AbsDiff precisa da imagem anterior")
            sys.exit(1)

        anterior = sys.argv[4]
        if not os.path.exists(anterior):
            print("Imagem anterior não encontrada")
            sys.exit(1)

        img_ant = cv2.imread(anterior, cv2.IMREAD_COLOR)
        if img_ant is None:
            print("Erro ao abrir imagem anterior")
            sys.exit(1)

        out = absdiff_img(img_ant, img)

    else:
        print("Operação inválida")
        sys.exit(1)

    cv2.imwrite(saida, out)
    print("OK")

if __name__ == "__main__":
    main()