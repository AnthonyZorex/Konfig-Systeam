
function addBlock()
{
    blockCount++;

    let countKeySelect = document.getElementById("countKeySelect");
    let x = Number(countKeySelect.value);
    x++;
    countKeySelect.value = x;
    let block_count_item = document.querySelectorAll(".block");

    let newBlock = document.createElement('div');
    newBlock.id = 'TexCount-' + blockCount;
    newBlock.innerHTML = `
                                       <div  class="TexCount">
                                        <input type="text" readonly required  name="FurNameKey" id="keyPoint" value="${blockCount}" />
                                    <div id="TexInputKey">
                                        <input id="inputS" required name="NameKey" onchange="chekedNameArienKey(event.target.value,${blockCount})" value="Schlüssel ${blockCount}" placeholder="Name des Schlüssels" type="text" />
                                    </div>
                                    <div>
                                        <input id="inputCount" required name="CountKey" value="1" type="text" />
                                    </div>
                                </div> `;

    document.getElementById('itemKeyBlock').appendChild(newBlock);

    let horizont = document.querySelectorAll(`.horizontal`);

    for (let i = 1; i <= block_count_item.length; i++)
    {

        let newBlockTur = document.createElement('div');

        newBlockTur.innerHTML = ` <input type="checkbox"  class="konfiguratorSelect" onmouseover="drawLines(${i},${blockCount})"  onmouseout="hideLines(${i},${blockCount})" id=${i}checkbox${blockCount}>
        <input type="hidden"  value="false" id="I${i}checkbox${blockCount}">`;

        newBlockTur.id = `${i}.checkboxContainer${blockCount}`;

        newBlockTur.classList.add('checkboxContainer');

        horizont[i-1].appendChild(newBlockTur);

        document.getElementById('InfoValue').appendChild(horizont[i - 1]);

        createCustomCheckbox(`${i}checkbox${blockCount}`);
    }

}
function removeBlock()
{
    let block_count_item = document.querySelectorAll(".block");
    let horizont = document.querySelectorAll(`.horizontal`);

    console.log(horizont[0].childNodes);

    let blockToRemove = document.getElementById('TexCount-' + blockCount);
    let countKeySelect = document.getElementById("countKeySelect");
    let x = Number(countKeySelect.value);
    x--;
    countKeySelect.value = x;

    if (blockToRemove)
    {
        blockToRemove.remove();
        if (block_count_item.length > 1)
        {
            for (let f = 1; f <= block_count_item.length;)
            {
                let blockToRemoveTurValue = horizont[f - 1].childNodes[horizont[f - 1].childNodes.length - 1];
                blockToRemoveTurValue.remove();

                f++;

            }

        }

        else {
            let blockToRemoveTurValue = document.getElementById(`${blockTur}.checkboxContainer${blockCount}`);
            blockToRemoveTurValue.remove();
        }

        blockCount--;
    }

}
function selectKey(value) {
    localStorage.clear();

    if (value > blockCount)
    {
        for (let i = blockCount; i < value; i++)
        {
            blockCount++;

            let newBlock = document.createElement('div');
            newBlock.id = 'TexCount-' + blockCount;
            newBlock.innerHTML = `
                                       <div  class="TexCount">
                                                     <input type="text" required readonly  name="FurNameKey" id="keyPoint" value="${blockCount}" />
                                    <div id="TexInputKey">
                                                    <input id="inputS" required name="NameKey" onchange="chekedNameArienKey(event.target.value,${blockCount})" value="Schlüssel ${blockCount}" placeholder="Name des Schlüssels" type="text" />
                                    </div>
                                    <div>
                                       <input id="inputCount" required name="CountKey" value="1" type="text" />
                                    </div>
                                </div>

                `;

            document.getElementById('itemKeyBlock').appendChild(newBlock);

            let horizont = document.getElementById(`${blockTur}horizontal`);

            horizont.draggable = "true";

            for (let i = 1; i <= blockTur; i++)
            {

                let newBlockTur = document.createElement('div');

                newBlockTur.innerHTML = ` <input type="checkbox" class="konfiguratorSelect" onmouseover="drawLines(${i},${blockCount})"  onmouseout="hideLines(${i},${blockCount})" id=${i}checkbox${blockCount}>
                                            <input type="hidden"  value="false" id="I${i}checkbox${blockCount}">`;

                newBlockTur.id = `${i}.checkboxContainer${blockCount}`;

                newBlockTur.classList.add('checkboxContainer');

                document.getElementById(`${i}horizontal`).appendChild(newBlockTur);

                document.getElementById('InfoValue').appendChild(horizont);

                createCustomCheckbox(`${i}checkbox${blockCount}`);
            }

            blocksKey = containerKey.querySelectorAll('.horizontal');

            blocksKey.forEach(block => {
                block.addEventListener('dragstart', handleDragStartKey);
                block.addEventListener('dragover', handleDragOverKey);
                block.addEventListener('drop', handleDropKey);
                block.addEventListener('dragend', handleDragEndKey);
            });

        }
    }
    else {

        for (let i = blockCount; i > value; i--)
        {
            blockCount--;

            let key = document.querySelectorAll(".TexCount");
            key[key.length - 1].remove();

            let checkboxContainers = document.querySelectorAll(".horizontal");

            checkboxContainers.forEach(container => {

                let checkboxes = container.querySelectorAll('.checkboxContainer');
                checkboxes[checkboxes.length - 1].remove();
            });
        }

    }
}
document.getElementById('removeButton').addEventListener('click', removeBlock);
document.getElementById('addButton').addEventListener('click', addBlock);