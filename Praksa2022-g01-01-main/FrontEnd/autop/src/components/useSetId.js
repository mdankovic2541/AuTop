import { useState } from 'react';

export default function useSetId() {
  const getUserId = () => {
    const idString = sessionStorage.getItem('id');

    if(idString === 'undefined'){
      console.log("Error in useSetId")
    }
    else{
    const userId = JSON.parse(idString);   
    return userId
    }
  };

  const [id, setUserId] = useState(getUserId());

  const saveId = userId => {
    sessionStorage.setItem('id', JSON.stringify(userId));
    setUserId(userId);
  };

  return {
    setUserId: saveId,
    id
  }
}