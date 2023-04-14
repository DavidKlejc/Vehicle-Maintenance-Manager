import React from "react";
import { useEffect, useState } from "react";
import { IOwner } from "../types/global.typing";
import httpModule from "../helpers/http.module";

const Owners = () => {
  const [owners, setOwners] = useState<IOwner[]>([]);

  useEffect(() => {
    httpModule
      .get<IOwner[]>("/Owner/Get")
      .then((response) => {
        setOwners(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return (
    <div>
      <h1>Owners</h1>
      <ul className="owners">
        {owners.map((owner) => (
          <li key={owner.id}>
            {owner.firstName} {owner.lastName}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Owners;
