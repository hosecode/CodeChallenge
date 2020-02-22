import collections

class Solution:
  def canConstruct(self, note, mag):
    mag_dict = collections.defaultdict(int)
    for char in mag:
      mag_dict[char]+=1
    for char in note:
      mag_dict[char]-=1
      if mag_dict[char]<0:
        return False
    return True

print(Solution().canConstruct('aa', 'aab'))
# True
