#/usr/local/bin/python
#encoding=utf-8

import re, os

file_name = __file__.split('/')[-1].replace('py', 'txt')

with open(file_name) as f:
    file_content = f.read()    
print file_content

pattern = r"sales."

print "===== %s ======" %pattern
pattern = re.compile(pattern)
result = re.findall(pattern, file_content)
print result


pattern_1 = r".a."

print "===== %s ======" %pattern_1
pattern = re.compile(pattern_1)
result = re.findall(pattern_1, file_content)
print result


pattern_2 = r".a.."

print "===== %s ======" %pattern_2
pattern = re.compile(pattern_2)
result = re.findall(pattern_2, file_content)
print result


pattern_3 = r".a.\.xls"
print "===== %s ======" %pattern_3
pattern = re.compile(pattern_3)
result = re.findall(pattern_3, file_content)
print result
