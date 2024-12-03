import re

pat0 = r"mul\(\d{1,3},\d{1,3}\)"
pat1 = r"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)"

with open('inputdata2.txt', 'r') as f:
    text = f.read()

def mul(x, y): return x * y
def print_sum(exprs): print(sum(eval(expr) for expr in exprs))

matches0 = re.findall(pat0, text)
print('part 1:')
print_sum(matches0)

matches1 = re.findall(pat1, text)
do = True
enabled = []
for m in matches1:
    if m == "don't()":
        do = False
    elif m == "do()":
        do = True
    else:
        if do:
            enabled.append(m)
print('part 2')
print_sum(enabled)