#/usr/local/bin/python
#encoding=utf-8

import re, os

file_name = __file__.split('/')[-1].replace('py', 'txt')

with open(file_name) as f:
    file_content = f.read()    
print file_content

pattern = r"myArray\[0\]"

print "===== %s ======" %pattern
pattern = re.compile(pattern)
result = re.findall(pattern, file_content)
print result

pattern = r"myArray\[[0-9]\]"

print "===== %s ======" %pattern
pattern = re.compile(pattern)
result = re.findall(pattern, file_content)
print result


string = r"\home\ben\\\\sales"
pattern_3 = r"\\"


print "===== %s ======" %pattern_3
pattern = re.compile(pattern_3)
result = re.findall(pattern_3, file_content)
print result



