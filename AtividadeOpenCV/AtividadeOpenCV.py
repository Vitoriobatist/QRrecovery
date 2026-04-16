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

    rec = cv2.remap(img, map_x, map_y, interpolation=cv2.INTER_NEAREST)
    return rec

def to_gray(img):
    if len(img.shape) == 3:
        return cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    return img

def mediana(img):
    return cv2.medianBlur(img, 3)

def gaussiano(img):
    return cv2.GaussianBlur(img, (3, 3), 0)

def threshold_preto(img):
    gray = to_gray(img)
    _, out = cv2.threshold(gray, 127, 255, cv2.THRESH_BINARY)
    return out

def dilatacao(img):
    kernel = np.ones((3, 3), np.uint8)
    return cv2.dilate(img, kernel, iterations=1)

def erosao(img):
    kernel = np.ones((3, 3), np.uint8)
    return cv2.erode(img, kernel, iterations=1)

def absdiff_img(img1, img2):
    g1 = to_gray(img1)
    g2 = to_gray(img2)
    return cv2.absdiff(g1, g2)

def main():
    if len(sys.argv) < 4:
        print("Uso: python processar.py <operacao> <entrada> <saida> [anterior]")
        sys.exit(1)

    operacao = sys.argv[1]
    entrada = sys.argv[2]
    saida = sys.argv[3]

    if not os.path.exists(entrada):
        print("Arquivo de entrada não encontrado.")
        sys.exit(1)

    img = cv2.imread(entrada, cv2.IMREAD_COLOR)
    if img is None:
        print("Não foi possível abrir a imagem.")
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
        gray = to_gray(img)
        out = dilatacao(gray)

    elif operacao == "erosao":
        gray = to_gray(img)
        out = erosao(gray)

    elif operacao == "absdiff":
        if len(sys.argv) < 5:
            print("Absdiff precisa da imagem anterior.")
            sys.exit(1)

        anterior = sys.argv[4]
        if not os.path.exists(anterior):
            print("Arquivo anterior não encontrado.")
            sys.exit(1)

        img_ant = cv2.imread(anterior, cv2.IMREAD_COLOR)
        if img_ant is None:
            print("Não foi possível abrir a imagem anterior.")
            sys.exit(1)

        out = absdiff_img(img_ant, img)

    else:
        print("Operação inválida.")
        sys.exit(1)

    cv2.imwrite(saida, out)
    print("OK")

if __name__ == "__main__":
    main()