class Solution:
  def twoSum(self, nums, target):
    dict = {}
    for i, num in enumerate(nums):
      if target-num in dict:
        return [dict[target-num],i]
      else:
        dict[num]=i

nums = [2,7,11,15]
target=9
print(Solution().twoSum(nums,target))
