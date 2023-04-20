import React from "react";
import { useEffect, useState } from "react";
import { IOwner } from "../types/global.typing";
import httpModule from "../helpers/http.module";
import "../styles/Owners.css";
import { Link } from "react-router-dom";
import { useAtom } from "jotai";
import { ownerIDAtom } from "../App";

const Owners = () => {
  const [owners, setOwners] = useState<IOwner[]>([]);
  const [ownerID, setOwnerID] = useAtom(ownerIDAtom);

  const handleSelectedOwner = (owner_id: string) => {
    console.log(owner_id);
    setOwnerID(owner_id);
  };

  useEffect(() => {
    httpModule
      .get<IOwner[]>("/Owner/Get")
      .then((response) => {
        setOwners(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [ownerID]);

  return (
    <div className="owners">
      <h1>Owners</h1>
      <ul className="owners-list">
        {owners.map((owner) => (
          <li key={owner.id} className="individual-owner">
            <div className="owner-names">
              {owner.firstName} {owner.lastName}
            </div>
            <Link
              onClick={() => handleSelectedOwner(owner.id)}
              to={`/Vehicle/${owner.id}`}
              className="button"
            >
              View
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Owners;
