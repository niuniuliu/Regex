#/usr/local/bin/python
#encoding=utf-8

import re, os

file_name = __file__.split('/')[-1].replace('py', 'txt')

with open(file_name) as f:
    file_content = f.read()    
print file_content

pattern = r"Ben"

print "===== %s ======" %pattern
pattern = re.compile(pattern)
result = re.search(pattern, file_content)
print result.group()

pattern_1 = r"my"
print "===== %s ======" %pattern_1
pattern = re.compile(pattern_1)
result = re.search(pattern_1, file_content)
print result.group()
