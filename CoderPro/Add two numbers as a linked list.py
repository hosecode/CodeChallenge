class Node(object):
  def __init__(self, x):
    self.val = x
    self.next = None

Zero = Node(0)

class Solution:
  def addTwoNumbers(self, l1, l2):
    carry=0
    node=None
    while l2 or l1 or carry>0:
      l1val = (l1 or Zero).val
      l2val = (l2 or Zero).val

      
      val = l1val + l2val + carry
      print (l1val, l2val, carry, sep='+', end="="+str(val)+'\n')
      if val > 9:
        carry = 1
        val-=10
      else:
        carry=0
      if not node:
        head = node = Node(val)
      else:
        node.next = Node(val)
        node=node.next
      if l1:
        l1 = l1.next
      if l2:
        l2 = l2.next  

    return head;


l1 = Node(2)
l1.next = Node(4)
l1.next.next = Node(3)

l2 = Node(5)
l2.next = Node(6)
l2.next.next = Node(4)

answer = Solution().addTwoNumbers(l1, l2)
while answer:
  print(answer.val, end=' ')
  answer = answer.next
print()
# 7 0 8
