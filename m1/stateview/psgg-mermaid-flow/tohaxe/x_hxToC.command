#!/bin/sh
cd `dirname $0`

echo :
echo : Remove Work Foloder ? [Y or enter]

read input

if [ -z $input ] ; then
    echo : skip    
elif [ $input = 'Y' ]  ||  [ $input = 'y' ] ; then
    echo : remove out/c 
    rm -f ./out/c || true
fi

echo : Remove hx/RegexUtil, hx/SortUtil for replacement

rm -f ./src/hx/RegexUtil.hx || true
rm -f ./src/hx/SortUtil.hx || true

echo :
echo : Compile
echo :
Haxe -p src/sys -p src/hx_alt -p src/cs2hx_src -p src/hx -p src/test   -m Program  --cpp out/c 

echo :
echo : Run  [Enter]
read wait
./out/c/Program
read wait
