#!/usr/bin/env python
# -*- coding: iso-8859-15 -*-

#---------------------------------------------------------------------------
#  LatexDB 0.3  --  enhances LaTeX with database access features
#  Copyright (C) 2003-2006  Hans-Georg Eßer <h.g.esser@gmx.de>
#
#  Contributors:
#  Gerhard Kirchmann <gerhard.kirchmann@arcor.de>:
#    PostgreSQL code and code cleanup (using Cursors)
#  Francois Meyer <fmeyer@obs-besancon.fr>
#    Fix for a problem with recursion in the preparser
#
#  This program is free software; you can redistribute it and/or modify
#  it under the terms of the GNU General Public License as published by
#  the Free Software Foundation; either version 2 of the License, or
#  (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU General Public License for more details.
#
#  You should have received a copy of the GNU General Public License
#  along with this program; if not, write to the Free Software
#  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#---------------------------------------------------------------------------


"""
preparse:

This is the pre-parser. Its single goal is to find LaTeXDB
statements that use more than one line or occur in the middle
of a line/lines. It will change the file so that the whole
LaTeXDB statements sits on one (!)  line of its own.
"""

import re
from string import split
from sys import stdin



### removeComments
### Argument: line l
### return value: l without trailing comments
def removeComments(l):
    newString=""
    comment=0
    # dont remove comments of type %!texDB!
    if l[0:8]=="%!texDB!": return l
    # otherwise search for first % and skip it and rest
    for ch in l:
        if ch=="%":
            comment=1
        elif not comment:
            newString+=ch
    return newString

### removeComments ends



### finishParseFor
### Arguments: left and right half of string
### Initial argument: "" and String beginning with \texdbfor{
### Function: go on parsing until block is done
### Return value: rest of last line after end of block
def finishParseFor(left,right,blocks,depth):
    while depth>-1:
       if right == "": right=" "+removeComments(stdin.readline())
       # get more input
       
       c=right[0]         # first char of right part
       if c == "{":
           if depth == 0: blocks=blocks+1
           depth=depth+1
       elif c == "}":
           depth=depth-1
       elif c == "\n": c=""  # turn newline into space
       left=left+c
       right=right[1:]
       if (blocks == 2) and (depth == 0):   # done
           return (left,right,-1,-1)
    return (left,right,-1,-1) 
    
### finishParseFor ends



### finishParseDef
### Arguments: left and right half of string
### Initial argument: "" and String beginning with \texdbdef{
### Function: go on parsing until block is done
### Return value: rest of last line after end of block
def finishParseDef(left,right,blocks,depth):
    if depth == -1: return (left,right,-1,-1)  # end of loop
    if right == "": right=removeComments(stdin.readline())
    # get more input

    c=right[0]         # first char of right part
    if c == "{":
        if depth == 0: blocks=blocks+1
        depth=depth+1
    elif c == "}":
        depth=depth-1
    elif c == "\n": c=""  # turn newline into space
    left=left+c
    right=right[1:]
    if (blocks == 3) and (depth == 0):   # done
        return (left,right,-1,-1)
    return finishParseDef(left,right,blocks,depth)
    
### finishParseDef ends




matchFor=re.compile(r"(.*)(\\texdbfor{.*)")
matchDef=re.compile(r"(.*)(\\texdbdef{.*)")

input=stdin.readline()
while input:
    input=removeComments(input)
    mDef=matchDef.match(input)
    if mDef:
        # we found a \texdbdef command
        g=mDef.groups()
        # g[0] contains part before \texdbdef...
        # g[1] contains part from  \texdbdef, including it
        if g[0] != "": print g[0]
        print "%% BEGIN TeXDBdef"
        left,right,blocks,depth=finishParseDef("",g[1],0,0)
        print left
        print "%% EHD TeXDBdef"
        input=right
    mFor=matchFor.match(input)
    if mFor:
        # we found a \texdbfor command
        # print matchFor.split(input)
        g=mFor.groups()
        # g[0] contains part before \texdbfor...
        # g[1] contains part from  \texdbfor, including it
        if g[0] != "": print g[0]
        # print "%% BEGIN TeXDBfor"
        left,right,blocks,depth=finishParseFor("",g[1],0,0)
        print left
        # print "%% EHD TeXDBfor"
        input=right
    if (not mFor) and (not mDef):
        print input[:-1]
        input=0 # make loop read next line
    if input=="": print ""
    if not input: input=stdin.readline()

