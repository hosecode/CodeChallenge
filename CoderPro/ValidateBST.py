# Definition for a binary tree node.
class TreeNode:
  def __init__(self, x):
    self.val = x
    self.left = None
    self.right = None

infmin = float('-inf');
infmax = float('inf');

class Solution:
  def isValidBST(self, root):
    def helper(node, lower, upper):
      if not node:
       return True
      val = node.val
      if val < lower or val>upper:
        return False
      if not helper(node.left, lower, val):
        return False
      if not helper(node.right, val, upper):
        return False;
      return True;
    return helper(root, infmin, infmax)

node = TreeNode(5)
node.left = TreeNode(4)
node.right = TreeNode(7)
print(Solution().isValidBST(node))
# True

node = TreeNode(5)
node.left = TreeNode(1)
node.right = TreeNode(4)
node.right.left = TreeNode(3)
node.right = TreeNode(6)
print(Solution().isValidBST(node))
# True

node = TreeNode(5)
node.left = TreeNode(1)
node.right = TreeNode(6)
node.right.left = TreeNode(4)
node.right.right = TreeNode(7)
print(Solution().isValidBST(node))
# False
